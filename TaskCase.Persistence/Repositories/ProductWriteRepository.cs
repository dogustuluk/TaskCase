using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;
public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(AppDbContext context) : base(context)
    {
    }
}
