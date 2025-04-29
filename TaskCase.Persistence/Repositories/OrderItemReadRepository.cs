using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
{
    public OrderItemReadRepository(AppDbContext context) : base(context)
    {
    }
}
