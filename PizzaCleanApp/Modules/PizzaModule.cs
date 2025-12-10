using Microsoft.AspNetCore.Http.HttpResults;

namespace PizzaCleanApp.API.Modules
{
    public class PizzaModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/pizzas")
                .WithOpenApi()
                .WithTags("Pizzas");
        }
    }
}
