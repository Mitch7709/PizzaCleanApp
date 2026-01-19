using PizzaCleanApp.API.Configuration;
using PizzaCleanApp.Infrastructure.Identity;

namespace PizzaCleanApp.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DBOptions>()
            .Bind(configuration.GetSection("Database"))
            .ValidateDataAnnotations();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateDataAnnotations();

        return services;
    }
}
