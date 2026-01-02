using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Pizzas.Update;

public class UpdatePizzaUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdatePizzaResponse>> ExecuteAsync(long id, UpdatePizzaRequest request)
    {
        if (request is null)
            return Result.Failure(ErrorType.ValidationError, "UpdatePizzaRequest cannot be null");

        var pizza = await dbContext.Set<Pizza>().FindAsync(id);
        if (pizza is null)
            return Result.Failure(ErrorType.NotFound, $"Pizza with id {id} was not found");

        pizza.Name = request.Name;
        pizza.Description = request.Description;
        pizza.BasePrice = request.BasePrice;
        pizza.IsActive = request.IsActive;
        await dbContext.SaveChangesAsync();

        return Result<UpdatePizzaResponse>.Success(
            new UpdatePizzaResponse(pizza.Id, pizza.Name, pizza.Description, pizza.BasePrice, pizza.IsActive));
    }
}
