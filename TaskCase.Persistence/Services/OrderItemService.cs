using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TaskCase.Application.Attributes;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Repositories;
using TaskCase.Application.Services;
using TaskCase.Domain.Entities;

namespace TaskCase.Persistence.Services;

[Service(ServiceLifetime.Scoped)]
public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemReadRepository _readRepository;
    private readonly IOrderItemWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemReadRepository readRepository, IOrderItemWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public Task<OptResult<OrderItem>> CreateOrderItemAsync(OrderItem model)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<OrderItem>> DeleteOrderItemAsync(object value, int deleteType)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetAllOrderItemAsync(Expression<Func<OrderItem, bool>>? predicate, string? include)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<PaginatedList<OrderItem>>> GetAllPagedOrderItemAsync(OrderItem model)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<OrderItem>> GetByIdOrGuid(object criteria)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<OrderItem>> UpdateOrderItemAsync(OrderItem model)
    {
        throw new NotImplementedException();
    }
}
