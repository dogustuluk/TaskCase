using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TaskCase.Application.Attributes;
using TaskCase.Application.Common.Extensions;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Repositories;
using TaskCase.Application.Services;
using TaskCase.Domain.Entities;

namespace TaskCase.Persistence.Services;

[Service(ServiceLifetime.Scoped)]
public class OrderService : IOrderService
{
    private readonly IOrderReadRepository _readRepository;
    private readonly IOrderWriteRepository _writeRepository;

    private readonly IProductReadRepository _readProductRepository;
    private readonly IProductWriteRepository _writeProductRepository;


    private readonly IMapper _mapper;

    public OrderService(IOrderReadRepository readRepository, IOrderWriteRepository writeRepository, IMapper mapper, IProductReadRepository readProductRepository, IProductWriteRepository writeProductRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
        _readProductRepository = readProductRepository;
        _writeProductRepository = writeProductRepository;
    }

    public async Task<OptResult<Order>> CreateOrderAsync(Order model)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            model.UserId = 1;
            model.Guid = Guid.NewGuid();
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;

            foreach (var item in model.Items)
            {
                var product = await _readProductRepository.GetByIdAsync(item.ProductId);

                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException($"Ürün stok yetersiz: {product.Name}");

                item.UnitPrice = product.Price;

                product.Stock -= item.Quantity;
                _writeProductRepository.Update(product);

                item.Order = model;
                item.Guid = Guid.NewGuid();
                item.CreatedDate = DateTime.UtcNow;
                item.UpdatedDate = DateTime.UtcNow;
            }

            await _writeRepository.AddAsync(model);

            await _writeRepository.SaveChanges();
            await _writeProductRepository.SaveChanges();

            return await OptResult<Order>.SuccessAsync(model);
        });
    }


    public async Task<OptResult<Order>> DeleteOrderAsync(object value, int deleteType)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            if (!(value is int id))
                throw new ArgumentException("Geçersiz Sipariş ID");

            var order = await _readRepository.GetEntityWithIncludeAsync(o => o.Id == id && o.UserId == 1, "Items");

            if (order == null)
                throw new KeyNotFoundException("Sipariş bulunamadı");

            foreach (var item in order.Items)
            {
                var prod = await _readProductRepository.GetByIdAsync(item.ProductId);
                prod.Stock += item.Quantity;
                _writeProductRepository.Update(prod);
            }

            _writeRepository.Remove(order);

            await _writeProductRepository.SaveChanges();
            await _writeRepository.SaveChanges();

            return await OptResult<Order>.SuccessAsync(order);
        });
    }

    public async Task<List<Order>> GetAllOrderAsync(Expression<Func<Order, bool>>? predicate, string? include)
    {
        var query = _readRepository.Table
        .Where(o => o.UserId == 1)
        .Include(o => o.Items)
            .ThenInclude(i => i.Product);

        return await query.ToListAsync();


    }

    public Task<OptResult<PaginatedList<Order>>> GetAllPagedProductAsync(Order model)
    {
        throw new NotImplementedException();
    }

    public async Task<OptResult<Order>> GetByIdOrGuid(object criteria)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            if (!(criteria is int id))
                throw new ArgumentException("Geçersiz Sipariş ID");

            var order = await _readRepository.GetEntityWithIncludeAsync(o => o.Id == id && o.UserId == 1, "Items");

            if (order == null)
                return await OptResult<Order>.FailureAsync(order, "Veri Bulunamadı");

            return await OptResult<Order>.SuccessAsync(order);
        });
    }

    public Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<Product>> UpdateOrderAsync(Order model)
    {
        throw new NotImplementedException();
    }
}
