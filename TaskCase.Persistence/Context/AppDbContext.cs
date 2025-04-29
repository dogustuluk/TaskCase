using Microsoft.EntityFrameworkCore;
using TaskCase.Domain.Entities;

namespace TaskCase.Persistence.Context;
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public override int SaveChanges()
    {
        ConvertDateTimesToUtc();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDateTimesToUtc();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDateTimesToUtc()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime))

                    {
                        var dt = (DateTime)property.CurrentValue;
                        if (dt.Kind == DateTimeKind.Unspecified || dt.Kind == DateTimeKind.Local)
                        {
                            property.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        }
                    }
                    else if (property.Metadata.ClrType == typeof(DateTime?))
                    {
                        var dt = (DateTime?)property.CurrentValue;
                        if (dt.HasValue && (dt.Value.Kind == DateTimeKind.Unspecified || dt.Value.Kind == DateTimeKind.Local))
                        {
                            property.CurrentValue = DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc);
                        }
                    }
                }
            }
        }
    }
}
