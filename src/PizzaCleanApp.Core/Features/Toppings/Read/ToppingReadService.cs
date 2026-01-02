using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Toppings.Read;

public class ToppingReadService(IDbContext dbContext)
{
    public async Task<IReadOnlyList<ToppingResponse>> GetAllAsync()
    {
        return await dbContext.Set<Topping>()
            .Select(t => new ToppingResponse(t.Id, t.Name, t.Price, t.Calories, t.CategoryType, t.IsActive))
            .ToListAsync();
    }

    public async Task<Result<ToppingResponse>> GetByIdAsync(long id)
    {
        var topping = await dbContext.Set<Topping>()
            .Where(t => t.Id == id)
            .Select(t => new ToppingResponse(t.Id, t.Name, t.Price, t.Calories, t.CategoryType, t.IsActive))
            .FirstOrDefaultAsync();

        if (topping == null)
        {
            return Result.Failure(ErrorType.NotFound, $"Topping with id {id} not found");
        }

        return topping;
    }
}
