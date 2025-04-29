using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskCase.Extensions.StartupExtensions;

public static class JsonStartupExtension
{
    public static void JsonOptionsStartupExtension(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; //null degerleri api'den geri dondurmez, daha performansli olmasi icin.
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
    }
}

