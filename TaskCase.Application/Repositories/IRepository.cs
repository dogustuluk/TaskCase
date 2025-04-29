using Microsoft.EntityFrameworkCore;
using TaskCase.Domain.Entities;

namespace TaskCase.Application.Repositories;
public interface IRepository<T> where T : IEntity
{
    DbSet<T> Table { get; }
}