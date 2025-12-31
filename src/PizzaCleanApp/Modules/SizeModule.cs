
using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.Core.Features.Sizes.Read;

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
    }

    private static async Task<Ok<IEnumerable<SizeResponse>>> GetAllSizes(int? page, int? pageSize, SizeReadService service)
    {
        var result = await service.GetAllAsync(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
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
}
