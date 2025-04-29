using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(AppDbContext context) : base(context)
    {
    }
}
