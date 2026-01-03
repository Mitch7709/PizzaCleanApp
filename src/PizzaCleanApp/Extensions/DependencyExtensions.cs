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
using PizzaCleanApp.Core.Features.Crusts.Create;
using PizzaCleanApp.Core.Features.Crusts.Update;
using PizzaCleanApp.Core.Features.Crusts.Read;
using PizzaCleanApp.Core.Features.Crusts.Delete;
using PizzaCleanApp.Core.Features.Toppings.Create;
using PizzaCleanApp.Core.Features.Toppings.Update;
using PizzaCleanApp.Core.Features.Toppings.Read;
using PizzaCleanApp.Core.Features.Toppings.Delete;

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
            services.AddTransient<CreatePizzaUseCase>();
            services.AddTransient<PizzaReadService>();
            services.AddTransient<DeletePizzaUseCase>();
            services.AddTransient<UpdatePizzaUseCase>();

            services.AddValidatorsFromAssemblyContaining<CreateSizeValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateSizeValidator>();
            services.AddTransient<CreateSizeUseCase>();
            services.AddTransient<UpdateSizeUseCase>();
            services.AddTransient<SizeReadService>();
            services.AddTransient<DeleteSizeUseCase>();

            services.AddValidatorsFromAssemblyContaining<CreateCrustValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCrustValidator>();
            services.AddTransient<CreateCrustUseCase>();
            services.AddTransient<UpdateCrustUseCase>();
            services.AddTransient<CrustReadService>();
            services.AddTransient<DeleteCrustUseCase>();

            services.AddValidatorsFromAssemblyContaining<CreateToppingValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateToppingValidator>();
            services.AddTransient<CreateToppingUseCase>();
            services.AddTransient<UpdateToppingUseCase>();
            services.AddTransient<ToppingReadService>();
            services.AddTransient<DeleteToppingUseCase>();

            return services;
        }
    }
}
