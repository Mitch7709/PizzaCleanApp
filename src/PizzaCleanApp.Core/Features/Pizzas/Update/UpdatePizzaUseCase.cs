using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Pizzas.Update;

public class UpdatePizzaUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdatePizzaResponse>> ExecuteAsync(long id, UpdatePizzaRequest request)
    {
        if (request is null)
            return Result.Failure(ErrorType.ValidationError, "UpdatePizzaRequest cannot be null");

        var pizza = await dbContext.Set<Pizza>()
            .Include(p => p.PizzaToppings)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pizza is null)
            return Result.Failure(ErrorType.NotFound, $"Pizza with id {id} was not found");

        // Validate that all provided toppings exist and are active
        if (request.ToppingIds is not null && request.ToppingIds.Count > 0)
        {
            var existingActiveToppingIds = await dbContext.Set<Topping>()
                .Where(t => request.ToppingIds.Contains(t.Id) && t.IsActive)
                .Select(t => t.Id)
                .ToListAsync();

            var missing = request.ToppingIds.Except(existingActiveToppingIds).ToArray();
            if (missing.Length > 0)
            {
                return Result.Failure(
                    ErrorType.NotFound,
                    $"One or more toppings were not found or are inactive: [{string.Join(", ", missing)}]."
                );
            }
        }

        pizza.Name = request.Name;
        pizza.Description = request.Description;
        pizza.BasePrice = request.BasePrice;
        pizza.IsActive = request.IsActive;

        var currentToppingIds = pizza.PizzaToppings.Select(pt => pt.ToppingId).ToList();

        if (request.ToppingIds is not null && currentToppingIds != request.ToppingIds)
        {
            pizza.PizzaToppings.Clear();
            foreach (var toppingId in request.ToppingIds)
            {
                pizza.PizzaToppings.Add(new PizzaTopping
                {
                    PizzaId = pizza.Id,
                    ToppingId = toppingId
                });
            }
        }

        await dbContext.SaveChangesAsync();

        return Result<UpdatePizzaResponse>.Success(
            new UpdatePizzaResponse(pizza.Id, pizza.Name, pizza.Description, pizza.BasePrice, pizza.IsActive));
    }
}
