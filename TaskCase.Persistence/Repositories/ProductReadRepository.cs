using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(AppDbContext context) : base(context)
    {
    }
}
