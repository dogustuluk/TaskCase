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
public class ProductService : IProductService
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductReadRepository readRepository, IProductWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public Task<OptResult<Product>> CreateProductAsync(Product model)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<Product>> DeleteProductAsync(object value, int deleteType)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<PaginatedList<Product>>> GetAllPagedProductAsync(Product model)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllProductAsync(Expression<Func<Product, bool>>? predicate, string? include)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<Product>> GetByIdOrGuid(object criteria)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
    {
        throw new NotImplementedException();
    }

    public Task<OptResult<Product>> UpdateProductAsync(Product model)
    {
        throw new NotImplementedException();
    }
}
