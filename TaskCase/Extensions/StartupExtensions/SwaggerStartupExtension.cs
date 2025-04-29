using Microsoft.OpenApi.Models;

namespace TaskCase.Extensions.StartupExtensions;
public static class SwaggerStartupExtension
{
    public static void SwaggerOptionsExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerOpt =>
        {
            swaggerOpt.SwaggerDoc("taskCase_V1", new OpenApiInfo
            {
                Title = "TaskCase V1",
                Description = "TaskCase V1 Docs",
                Version = "Version_1"
            });
        });
    }
}
