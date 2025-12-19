using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.Core.Features.Pizzas.Read;

namespace PizzaCleanApp.API.Modules
{
    public class PizzaModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/pizzas")
                .WithOpenApi()
                .WithTags("Pizzas");

            group.MapGet("", GetAllPizzas);

            group.MapGet("/{id}", GetPizzaById);
        }

        private static async Task<Ok<IEnumerable<PizzaResponse>>> GetAllPizzas(int? page, int? pageSize, PizzaReadService service)
        {
            var result = await service.GetAll(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<PizzaResponse>, NotFound<string>>> GetPizzaById(long id, PizzaReadService service)
        {
            var result = await service.GetById(id);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }
            return TypedResults.NotFound(result.ErrorMessage);
        }
    }
}
