using System.Linq.Expressions;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Domain.Entities;

namespace TaskCase.Application.Services;
public interface IOrderService
{
    Task<OptResult<Order>> CreateOrderAsync(Order model);
    Task<OptResult<Product>> UpdateOrderAsync(Order model);
    Task<OptResult<Order>> DeleteOrderAsync(object value, int deleteType);
    Task<OptResult<Order>> GetByIdOrGuid(object criteria);
    Task<List<Order>> GetAllOrderAsync(Expression<Func<Order, bool>>? predicate, string? include);
    Task<OptResult<PaginatedList<Order>>> GetAllPagedProductAsync(Order model);
    Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
}
