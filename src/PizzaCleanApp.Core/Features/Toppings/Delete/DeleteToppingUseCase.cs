using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Toppings.Delete;

public class DeleteToppingUseCase(IDbContext dbContext)
{
    public async Task<Result> ExecuteAsync(long id)
    {
        var topping = await dbContext.Set<Topping>().FindAsync(id);
        if (topping is null)
            return Result.Failure(ErrorType.NotFound, $"Topping with id {id} was not found");

        dbContext.Set<Topping>().Remove(topping);
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }
}
