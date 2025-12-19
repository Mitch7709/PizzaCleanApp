using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Pizzas.Delete;

public class DeletePizzaUseCase(IDbContext dbContext)
{
    public async Task<Result> ExecuteAsync(long id)
    {
        var pizza = await dbContext.Set<Pizza>()
            .FindAsync(id);

        if (pizza == null)
        {
            return Result.Failure(ErrorType.NotFound, $"Pizza with id '{id}' was not found.");
        }
        
        dbContext.Set<Pizza>().Remove(pizza);
        await dbContext.SaveChangesAsync();

        return Result.Success();
    }
}
