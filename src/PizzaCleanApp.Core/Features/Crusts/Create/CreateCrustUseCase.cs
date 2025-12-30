using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Crusts.Create;

public class CreateCrustUseCase(IDbContext dbContext)
{
    public async Task<Result<CreateCrustResponse>> ExecuteAsync(CreateCrustRequest request)
    {
        var exists = await dbContext.Set<Crust>().AnyAsync(c => c.Name == request.Name);
        if (exists)
            return Result.Failure(ErrorType.Conflict, "A crust with the same name already exists.");

        var crust = new Crust
        {
            Name = request.Name,
            Calories = request.Calories,
            IsActive = request.IsActive
        };
        dbContext.Set<Crust>().Add(crust);
        await dbContext.SaveChangesAsync();
        return new CreateCrustResponse(crust.Id, crust.Name, crust.Calories, crust.IsActive);
    }
}
