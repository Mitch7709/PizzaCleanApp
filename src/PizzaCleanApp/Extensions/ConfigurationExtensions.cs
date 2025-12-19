using PizzaCleanApp.API.Configuration;

namespace PizzaCleanApp.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DBOptions>()
            .Bind(configuration.GetSection("Database"))
            .ValidateDataAnnotations();

        return services;
    }
}
