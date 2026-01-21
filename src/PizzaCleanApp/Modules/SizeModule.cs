using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Core.Features.Sizes.Create;
using PizzaCleanApp.Core.Features.Sizes.Delete;
using PizzaCleanApp.Core.Features.Sizes.Read;
using PizzaCleanApp.Core.Features.Sizes.Update;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.API.Modules;

public class SizeModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sizes")
            .WithOpenApi()
            .WithTags("Sizes");

        group.MapGet("", GetAllSizes);

        group.MapGet("/{id}", GetSizeById);

        group.MapPost("", CreateSize)
            .Validator<CreateSizeRequest>();

        group.MapPut("/{id}", UpdateSize)
            .Validator<UpdateSizeRequest>();

        group.MapDelete("/{id}", DeleteSize);
    }

    private static async Task<Ok<IReadOnlyList<SizeResponse>>> GetAllSizes(SizeReadService service)
    {
        var result = await service.GetAllAsync();
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<SizeResponse>, NotFound<string>>> GetSizeById(long id, SizeReadService service)
    {
        var result = await service.GetByIdAsync(id);
        if (result != null)
        {
            return TypedResults.Ok(result);
        }
        return TypedResults.NotFound($"Size with id {id} was not found.");
    }

    private static async Task<Ok<CreateSizeResponse>> CreateSize(CreateSizeRequest request, CreateSizeUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return TypedResults.Ok(result.Value);
    }

    private static async Task<Results<Ok<UpdateSizeResponse>, NotFound<string>>> UpdateSize(long id , UpdateSizeRequest request, UpdateSizeUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id, request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.ErrorMessage);
    }

    private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteSize(long id, DeleteSizeUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        
        if (result.IsSuccess)
            return TypedResults.NoContent();

        return result.ErrorType == ErrorType.NotFound
            ? TypedResults.NotFound(result.ErrorMessage)
            : TypedResults.BadRequest(result.ErrorMessage);
    }
}
