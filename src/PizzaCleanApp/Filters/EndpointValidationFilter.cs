using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace PizzaCleanApp.API.Filters;

public class EndpointValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    private IValidator<T> _validator => validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var inputData = context.Arguments.OfType<T>()
            .FirstOrDefault(a => a?.GetType() == typeof(T));

        if (inputData is not null)
        {
            ValidationResult result = await _validator.ValidateAsync(inputData);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary(),
                    statusCode: (int)HttpStatusCode.BadRequest);
            }
        }

        return await next.Invoke(context);
    }
}
