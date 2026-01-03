using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Core.Features.Toppings.Create;
using PizzaCleanApp.Core.Features.Toppings.Delete;
using PizzaCleanApp.Core.Features.Toppings.Read;
using PizzaCleanApp.Core.Features.Toppings.Update;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.API.Modules;

public class ToppingModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/toppings")
            .WithOpenApi()
            .WithTags("Toppings");

        group.MapGet("", GetAllToppings);

        group.MapGet("/{id}", GetToppingById);

        ((RouteHandlerBuilder)group.MapPost("", CreateTopping))
            .Validator<CreateToppingRequest>();

        ((RouteHandlerBuilder)group.MapPut("/{id}", UpdateTopping))
            .Validator<UpdateToppingRequest>();

        //group.MapDelete("/{id}", DeleteTopping);
    }

    private static async Task<Ok<IReadOnlyList<ToppingResponse>>> GetAllToppings(ToppingReadService service)
    {
        var result = await service.GetAllAsync();
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<ToppingResponse>, NotFound<string>>> GetToppingById(long id, ToppingReadService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<Ok<CreateToppingResponse>, BadRequest<string>, Conflict<string>>> CreateTopping(CreateToppingRequest request, CreateToppingUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);

        return result.ErrorType == ErrorType.Conflict
            ? TypedResults.Conflict(result.ErrorMessage)
            : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<Results<Ok<UpdateToppingResponse>, NotFound<string>>> UpdateTopping(long id, UpdateToppingRequest request, UpdateToppingUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id, request);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    //private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteTopping(long id, DeleteToppingUseCase useCase)
    //{
    //    var result = await useCase.ExecuteAsync(id);
    //    if (result.IsSuccess)
    //        return TypedResults.NoContent();

    //    return result.ErrorType == ErrorType.NotFound
    //        ? TypedResults.NotFound(result.ErrorMessage)
    //        : TypedResults.BadRequest(result.ErrorMessage);
    //}
}
