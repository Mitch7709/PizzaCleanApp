using PizzaCleanApp.Core.Features.Pizzas.Create;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.Infrastructure.Database;
using FluentValidation;
using PizzaCleanApp.Core.Features.Pizzas.Read;
using PizzaCleanApp.Core.Features.Pizzas.Delete;
using PizzaCleanApp.Core.Features.Pizzas.Update;
using PizzaCleanApp.Core.Features.Sizes.Create;
using PizzaCleanApp.Core.Features.Sizes.Update;
using PizzaCleanApp.Core.Features.Sizes.Read;
using PizzaCleanApp.Core.Features.Sizes.Delete;

namespace PizzaCleanApp.API.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<IDbContext, AppDbContext>();
            services.AddValidatorsFromAssemblyContaining<CreatePizzaValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePizzaValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateSizeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateSizeValidator>();

            services.AddTransient<CreatePizzaUseCase>();
            services.AddTransient<PizzaReadService>();
            services.AddTransient<DeletePizzaUseCase>();
            services.AddTransient<UpdatePizzaUseCase>();

            services.AddTransient<CreateSizeUseCase>();
            services.AddTransient<UpdateSizeUseCase>();
            services.AddTransient<SizeReadService>();
            services.AddTransient<DeleteSizeUseCase>();

            return services;
        }
    }
}
