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

    public async Task<CrustResponse?> GetByIdAsync(long id)
    {
        return await dbContext.Set<Crust>()
            .Where(c => c.Id == id)
            .Select(c => new CrustResponse(c.Id, c.Name, c.Calories, c.IsActive))
            .FirstOrDefaultAsync();
    }
}
