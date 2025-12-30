using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Sizes.Delete;

public class DeleteSizeUseCase(IDbContext dbContext)
{
    public async Task<Result> ExecuteAsync(long id)
    {
        var size = await dbContext.Set<Size>().FindAsync(id);
        if (size is null)
            return Result.Failure(ErrorType.NotFound, $"Size with id {id} was not found");

        dbContext.Set<Size>().Remove(size);
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }
}
