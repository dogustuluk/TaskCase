using System.Linq.Expressions;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Domain.Entities;

namespace TaskCase.Application.Services;
public interface IProductService
{
    Task<OptResult<Product>> CreateProductAsync(Product model);
    Task<OptResult<Product>> UpdateProductAsync(Product model);
    Task<OptResult<Product>> DeleteProductAsync(object value, int deleteType);
    Task<OptResult<Product>> GetByIdOrGuid(object criteria);
    Task<List<Product>> GetAllProductAsync(Expression<Func<Product, bool>>? predicate, string? include);
    Task<OptResult<PaginatedList<Product>>> GetAllPagedProductAsync(Product model);
    Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
}
