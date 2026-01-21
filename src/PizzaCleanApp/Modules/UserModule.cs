using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Core.Features.Users.Login;
using PizzaCleanApp.Core.Features.Users.Register;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.API.Modules;

public class UserModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", Register)
            .WithTags("Users")
            .WithOpenApi()
            .Validator<RegisterRequest>();

        app.MapPost("/login", Login)
            .WithTags("Users")
            .WithOpenApi()
            .Validator<LoginRequest>();
    }

    private static async Task<IResult> Login(LoginRequest request, LoginUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : Results.Json(new { Error = result.ErrorMessage, Code = "UNAUTHORIZED_ACCESS"}, statusCode: 401);
    }

    private static async Task<Results<Ok<RegisterResponse>, BadRequest<string>, UnprocessableEntity<string>>> Register(RegisterRequest request, RegisterUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        
        return result switch
                    {
            { IsSuccess: true } => TypedResults.Ok(result.Value),
            { IsFailure: true, ErrorType: ErrorType.ValidationError } => TypedResults.BadRequest(result.ErrorMessage),
            { IsFailure: true, ErrorType: ErrorType.DataError } => TypedResults.UnprocessableEntity(result.ErrorMessage),
            _ => throw new NotImplementedException(),
                    };
    }
}