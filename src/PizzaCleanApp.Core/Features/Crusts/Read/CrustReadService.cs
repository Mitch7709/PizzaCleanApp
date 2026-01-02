using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Crusts.Read;

public class CrustReadService(IDbContext dbContext)
{
    public async Task<IReadOnlyList<CrustResponse>> GetAllAsync()
    {
        return await dbContext.Set<Crust>()
            .Select(c => new CrustResponse(c.Id, c.Name, c.Calories, c.IsActive))
            .ToListAsync();
    }

    public async Task<Result<CrustResponse>> GetByIdAsync(long id)
    {
        var crust = await dbContext.Set<Crust>()
            .Where(c => c.Id == id)
            .Select(c => new CrustResponse(c.Id, c.Name, c.Calories, c.IsActive))
            .FirstOrDefaultAsync();

        if (crust == null)
        {
            return Result.Failure(ErrorType.NotFound, $"Crust with id {id} not found");
        }

        return crust;
    }
}
