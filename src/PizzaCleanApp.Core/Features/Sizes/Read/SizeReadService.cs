using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Sizes.Read;

public class SizeReadService(IDbContext dbContext)
{
    public async Task<IReadOnlyList<SizeResponse>> GetAllAsync()
    {
        return await dbContext.Set<Size>()
            .Select(s => new SizeResponse(s.Id, s.Name, s.Price, s.Calories))
            .ToListAsync();
    }

    public async Task<SizeResponse?> GetByIdAsync(long id)
    {
        return await dbContext.Set<Size>()
            .Where(s => s.Id == id)
            .Select(s => new SizeResponse(s.Id, s.Name, s.Price, s.Calories))
            .FirstOrDefaultAsync();
    }
}
