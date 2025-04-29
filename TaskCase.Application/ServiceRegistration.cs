
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskCase.Application.Common.Validators;

namespace TaskCase.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddMediatR(typeof(ServiceRegistration));
        serviceCollection.AddHttpClient();
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());


    }
}
