using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Toppings.Update;

public class UpdateToppingUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdateToppingResponse>> ExecuteAsync(long id, UpdateToppingRequest request)
    {
        var topping = await dbContext.Set<Topping>().FindAsync(id);
        if (topping is null)
            return Result.Failure(ErrorType.NotFound, $"Topping with id {id} was not found");

        topping.Name = request.Name;
        topping.Price = request.Price;
        topping.Calories = request.Calories;
        topping.CategoryType = request.CategoryType;
        topping.IsActive = request.IsActive;
        await dbContext.SaveChangesAsync();

        return Result<UpdateToppingResponse>.Success(new UpdateToppingResponse(topping.Id, topping.Name, topping.Price, topping.Calories, topping.CategoryType, topping.IsActive));
    }
}
