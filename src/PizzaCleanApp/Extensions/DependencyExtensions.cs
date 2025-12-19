using PizzaCleanApp.Core.Features.Pizzas.Create;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.Infrastructure.Database;
using FluentValidation;
using PizzaCleanApp.Core.Features.Pizzas.Read;
using PizzaCleanApp.Core.Features.Pizzas.Delete;
using PizzaCleanApp.Core.Features.Pizzas.Update;

namespace PizzaCleanApp.API.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<IDbContext, AppDbContext>();
            services.AddValidatorsFromAssemblyContaining<CreatePizzaValidator>();

            services.AddTransient<CreatePizzaUseCase>();
            services.AddTransient<PizzaReadService>();
            services.AddTransient<DeletePizzaUseCase>();
            services.AddTransient<UpdatePizzaUseCase>();

            return services;
        }
    }
}
