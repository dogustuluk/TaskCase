using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : IEntity
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        T model = await Table.FirstOrDefaultAsync(data => data.Id == id);
        return Remove(model);
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public int SqlCommand(string sql, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public Task<int> SqlCommandAsync(string sql, params object[]? parameters)
    {
        throw new NotImplementedException();
    }

    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public Task<bool> UpdateAsync(T model)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChanges(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async Task<T> AddAsyncReturnEntity(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        await SaveChanges();
        return entityEntry.Entity;
    }
}
