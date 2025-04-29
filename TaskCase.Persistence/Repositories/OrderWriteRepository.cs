using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(AppDbContext context) : base(context)
    {
    }
}
