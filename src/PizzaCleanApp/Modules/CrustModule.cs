using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Core.Features.Crusts.Create;
using PizzaCleanApp.Core.Features.Crusts.Delete;
using PizzaCleanApp.Core.Features.Crusts.Read;
using PizzaCleanApp.Core.Features.Crusts.Update;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.API.Modules;

public class CrustModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/crusts")
            .WithOpenApi()
            .WithTags("Crusts");

        group.MapGet("", GetAllCrusts);

        group.MapGet("/{id}", GetCrustById);

        ((RouteHandlerBuilder)group.MapPost("", CreateCrust))
            .Validator<CreateCrustRequest>();

        ((RouteHandlerBuilder)group.MapPut("/{id}", UpdateCrust))
            .Validator<UpdateCrustRequest>();

        group.MapDelete("/{id}", DeleteCrust);
    }

    private static async Task<Ok<IReadOnlyList<CrustResponse>>> GetAllCrusts(CrustReadService service)
    {
        var result = await service.GetAllAsync();
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<CrustResponse>, NotFound<string>>> GetCrustById(long id, CrustReadService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<Ok<CreateCrustResponse>, BadRequest<string>, Conflict<string>>> CreateCrust(CreateCrustRequest request, CreateCrustUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);

        return result.ErrorType == ErrorType.Conflict
            ? TypedResults.Conflict(result.ErrorMessage)
            : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<Results<Ok<UpdateCrustResponse>, NotFound<string>>> UpdateCrust(long id, UpdateCrustRequest request, UpdateCrustUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id, request);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteCrust(long id, DeleteCrustUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        if (result.IsSuccess)
            return TypedResults.NoContent();

        return result.ErrorType == ErrorType.NotFound
            ? TypedResults.NotFound(result.ErrorMessage)
            : TypedResults.BadRequest(result.ErrorMessage);
    }
}
