using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaCleanApp.API.Configuration;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Infrastructure.Database;

namespace PizzaCleanApp.API.Extensions;

public static class DBExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
           var dbOptions = serviceProvider.GetRequiredService<IOptions<DBOptions>>().Value;

           options.UseSqlServer(dbOptions.ConnectionString, sqlOptions =>
           {
               sqlOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount);
               sqlOptions.CommandTimeout(dbOptions.CommandTimeout);
           });
       });

       return services;
    }

    public static IApplicationBuilder UseDatabase(this IApplicationBuilder app)
    {
        if (app is not WebApplication webApplication)
        {
            throw new InvalidOperationException("UseDatabase must be called on a WebApplication instance.");
        }

        using var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
