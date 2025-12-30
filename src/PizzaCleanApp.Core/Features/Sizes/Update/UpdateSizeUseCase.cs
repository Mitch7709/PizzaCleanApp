using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Sizes.Update;

public class UpdateSizeUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdateSizeResponse>> ExecuteAsync(long id, UpdateSizeRequest request)
    {
        var size = await dbContext.Set<Size>().FindAsync(id);
        if (size is null)
            return Result.Failure(ErrorType.NotFound, $"Size with id {id} was not found");

        size.Name = request.Name;
        size.Price = request.Price;
        await dbContext.SaveChangesAsync();

        return Result<UpdateSizeResponse>.Success(new UpdateSizeResponse(size.Id, size.Name, size.Price, size.Calories));
    }
}
