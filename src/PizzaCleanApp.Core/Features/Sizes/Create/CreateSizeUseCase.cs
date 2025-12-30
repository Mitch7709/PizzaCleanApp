using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Sizes.Create;

public class CreateSizeUseCase(IDbContext dbContext)
{
    public async Task<Result<CreateSizeResponse>> ExecuteAsync(CreateSizeRequest request)
    {
        var exists = await dbContext.Set<Size>().AnyAsync(s => s.Name == request.Name);
        if (exists)
            return Result.Failure(ErrorType.Conflict, "A size with the same name already exists.");

        var size = new Size
        {
            Name = request.Name,
            Calories = request.Calories,
            Price = request.Price
        };
        dbContext.Set<Size>().Add(size);
        await dbContext.SaveChangesAsync();
        return new CreateSizeResponse(size.Id, size.Name, size.Price, size.Calories);
    }
}
