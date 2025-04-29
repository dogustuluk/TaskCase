using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskCase.Application.Common.Extensions;
using TaskCase.Application.Repositories;
using TaskCase.Persistence.Context;
using TaskCase.Persistence.Repositories;

namespace HospitalManagement.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));




            services.RegisterRepositories(typeof(IOrderReadRepository).Assembly, typeof(OrderReadRepository).Assembly);
            services.AddServicesInDbContextFromAttributes(Assembly.GetExecutingAssembly());

        }

        public static void InitializeSeedData(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //  SeedDataHelper.SeedDepartments(dbContext);
                //  SeedDataHelper.SeedCities(dbContext);
            }
        }

    }
}
