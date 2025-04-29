using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
{
    public OrderItemWriteRepository(AppDbContext context) : base(context)
    {
    }
}
