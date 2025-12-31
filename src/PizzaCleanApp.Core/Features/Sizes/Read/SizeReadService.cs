using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Sizes.Read;

public class SizeReadService(IDbContext dbContext)
{
    public async Task<IEnumerable<SizeResponse>> GetAllAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        return await dbContext.Set<Size>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
