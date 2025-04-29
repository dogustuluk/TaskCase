using Microsoft.Extensions.DependencyInjection;

namespace TaskCase.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ServiceAttribute : Attribute
{
    public ServiceLifetime lifetime { get; }
    public ServiceAttribute(ServiceLifetime lifetime)
    {
        this.lifetime = lifetime;
    }
}
