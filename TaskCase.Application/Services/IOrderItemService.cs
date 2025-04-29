using System.Linq.Expressions;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Domain.Entities;

namespace TaskCase.Application.Services;
public interface IOrderItemService
{
    Task<OptResult<OrderItem>> CreateOrderItemAsync(OrderItem model);
    Task<OptResult<OrderItem>> UpdateOrderItemAsync(OrderItem model);
    Task<OptResult<OrderItem>> DeleteOrderItemAsync(object value, int deleteType);
    Task<OptResult<OrderItem>> GetByIdOrGuid(object criteria);
    Task<List<OrderItem>> GetAllOrderItemAsync(Expression<Func<OrderItem, bool>>? predicate, string? include);
    Task<OptResult<PaginatedList<OrderItem>>> GetAllPagedOrderItemAsync(OrderItem model);
    Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
}
