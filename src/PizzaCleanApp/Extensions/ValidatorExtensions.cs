using PizzaCleanApp.API.Filters;

namespace PizzaCleanApp.API.Extensions;

public static class ValidatorExtensions
{
    public static RouteHandlerBuilder Validator<T>(this RouteHandlerBuilder builder)
        where T : class
    {
        builder.AddEndpointFilter<EndpointValidationFilter<T>>();
        return builder;
    }
}