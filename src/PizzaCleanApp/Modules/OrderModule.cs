using Microsoft.AspNetCore.Http.HttpResults;
using PizzaCleanApp.Core.Features.Orders.AddOrderItem;
using PizzaCleanApp.Core.Features.Orders.Read;

namespace PizzaCleanApp.API.Modules
{
    public class OrderModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/orders")
                .WithOpenApi()
                .WithTags("Orders");

            group.MapGet("/{id}", GetOrderById);

            group.MapPost("", AddItemToOrder);
        }
        private static async Task<Results<Ok<OrderResponse>, NotFound<string>>> GetOrderById(int id, OrderReadService service)
        {
            var result = await service.GetOrder(id);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.ErrorMessage);
        }

        private static async Task<Created> AddItemToOrder(AddOrderItemRequest request, AddOrderItemUseCase useCase)
        {
            await useCase.Execute(request);
            return TypedResults.Created();
        }
    }
}
