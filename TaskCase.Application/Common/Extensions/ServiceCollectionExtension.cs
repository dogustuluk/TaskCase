using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskCase.Application.Attributes;

namespace TaskCase.Application.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServicesInDbContextFromAttributes(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes().Where(a => a.GetCustomAttributes<ServiceAttribute>().Any()).ToList();

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<ServiceAttribute>();
            var interfaces = type.GetInterfaces();

            foreach (var interfaceType in interfaces)
            {
                switch (attribute.lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceType, type);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceType, type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceType, type);
                        break;
                }
            }
        }
    }
    public static void RegisterRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        // geçerli Assemblydeki tüm tipleri aliyorum
        var types = assemblies.SelectMany(a => a.GetTypes()).ToList();

        // interfaceleri ve classlari buluyorum
        var repositoryInterfaces = types.Where(t => t.IsInterface && t.Name.EndsWith("Repository")).ToList();
        var repositoryClasses = types.Where(t => t.IsClass && t.Name.EndsWith("Repository") && !t.IsAbstract).ToList();

        // her interface için classlari kaydediyorum
        foreach (var repositoryInterface in repositoryInterfaces)
        {
            var implementations = repositoryClasses.Where(c => repositoryInterface.IsAssignableFrom(c)).ToList();
            if (implementations.Count > 0)
            {
                foreach (var implementation in implementations)
                {
                    services.AddScoped(repositoryInterface, implementation);
                }
            }
            else
                Console.WriteLine($"No implementation found for {repositoryInterface.FullName}");
        }
    }
}

