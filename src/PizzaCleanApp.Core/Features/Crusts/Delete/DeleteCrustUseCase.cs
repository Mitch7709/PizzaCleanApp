using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Crusts.Delete;

public class DeleteCrustUseCase(IDbContext dbContext)
{
    public async Task<Result> ExecuteAsync(long id)
    {
        var crust = await dbContext.Set<Crust>().FindAsync(id);
        if (crust is null)
            return Result.Failure(ErrorType.NotFound, $"Crust with id {id} was not found");

        dbContext.Set<Crust>().Remove(crust);
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }
}
