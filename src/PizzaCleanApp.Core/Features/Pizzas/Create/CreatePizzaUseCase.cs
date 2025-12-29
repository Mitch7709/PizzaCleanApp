using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Pizzas.Create;

public class CreatePizzaUseCase(IDbContext dbContext)
{
    public async Task<Result<CreatePizzaResponse>> ExecuteAsync(CreatePizzaRequest request)
    {
        var existingPizza = await dbContext.Set<Pizza>()
            .AnyAsync(p => p.Name == request.Name);

        if (existingPizza)
        {
            return Result.Failure(ErrorType.Conflict, "A pizza with the same name already exists.");
        }

        var requestedToppingIds = request.ToppingIds?.Distinct().ToArray() ?? Array.Empty<long>();

        // Validate that all provided toppings exist and are active
        List<long> existingActiveToppingIds = new();
        if (requestedToppingIds.Length > 0)
        {
            existingActiveToppingIds = await dbContext.Set<Topping>()
                .Where(t => requestedToppingIds.Contains(t.Id) && t.IsActive)
                .Select(t => t.Id)
                .ToListAsync();

            var missing = requestedToppingIds.Except(existingActiveToppingIds).ToArray();
            if (missing.Length > 0)
            {
                return Result.Failure(
                    ErrorType.NotFound,
                    $"One or more toppings were not found or are inactive: [{string.Join(", ", missing)}]."
                );
            }
        }

        var pizza = new Pizza
        {
            Name = request.Name,
            Description = request.Description,
            BasePrice = request.BasePrice,
            IsActive = true
        };

        if (existingActiveToppingIds.Count > 0)
        {
            foreach (var toppingId in existingActiveToppingIds)
            {
                pizza.PizzaToppings.Add(new PizzaTopping
                {
                    ToppingId = toppingId
                });
            }
        }

        dbContext.Set<Pizza>().Add(pizza);
        await dbContext.SaveChangesAsync();

        return new CreatePizzaResponse(pizza.Id, pizza.Name, pizza.Description, pizza.BasePrice, existingActiveToppingIds);
    }
}
