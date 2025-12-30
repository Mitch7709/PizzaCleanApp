using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Crusts.Update;

public class UpdateCrustUseCase(IDbContext dbContext)
{
    public async Task<Result<UpdateCrustResponse>> ExecuteAsync(long id, UpdateCrustRequest request)
    {
        var crust = await dbContext.Set<Crust>().FindAsync(id);
        if (crust is null)
            return Result.Failure(ErrorType.NotFound, $"Crust with id {id} was not found");

        crust.Name = request.Name;
        crust.Calories = request.Calories;
        crust.IsActive = request.IsActive;
        await dbContext.SaveChangesAsync();

        return Result<UpdateCrustResponse>.Success(new UpdateCrustResponse(crust.Id, crust.Name, crust.Calories, crust.IsActive));
    }
}
