using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Toppings.Create;

public class CreateToppingUseCase(IDbContext dbContext)
{
    public async Task<Result<CreateToppingResponse>> ExecuteAsync(CreateToppingRequest request)
    {
        var exists = await dbContext.Set<Topping>().AnyAsync(t => t.Name == request.Name);
        if (exists)
            return Result.Failure(ErrorType.Conflict, "A topping with the same name already exists.");

        var topping = new Topping
        {
            Name = request.Name,
            Price = request.Price,
            Calories = request.Calories,
            CategoryType = request.CategoryType,
            IsActive = request.IsActive
        };
        dbContext.Set<Topping>().Add(topping);
        await dbContext.SaveChangesAsync();
        return new CreateToppingResponse(topping.Id, topping.Name, topping.Price, topping.Calories, topping.CategoryType, topping.IsActive);
    }
}
