using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Core.Features.Pizzas.Create;
using PizzaCleanApp.Core.Features.Pizzas.Read;
using PizzaCleanApp.Core.Features.Pizzas.Update;
using PizzaCleanApp.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.Core.Features.Pizzas.Delete;

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

            ((RouteHandlerBuilder)group.MapPut("/{id}", UpdatePizza))
                .Validator<UpdatePizzaRequest>();

            ((RouteHandlerBuilder)group.MapPost("", CreatePizza))
                .Validator<CreatePizzaRequest>();

            group.MapDelete("/{id}", DeletePizza);
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

        private static async Task<Ok<CreatePizzaResponse>> CreatePizza(CreatePizzaRequest request, CreatePizzaUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(request);
            return TypedResults.Ok(result.Value);
        }

        private static async Task<Results<Ok<UpdatePizzaResponse>, NotFound<string>>> UpdatePizza(long id , UpdatePizzaRequest request, UpdatePizzaUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(id, request);
            
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.ErrorMessage);
        }

        private static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeletePizza(long id, DeletePizzaUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(id);

            if (result.IsSuccess)
                return TypedResults.NoContent();

            return result.ErrorType == ErrorType.NotFound
                ? TypedResults.NotFound(result.ErrorMessage)
                : TypedResults.BadRequest(result.ErrorMessage);
        }
    }
}
