using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Pizzas.Read;

public class PizzaReadService(IDbContext dbContext)
{
    public async Task<IEnumerable<PizzaResponse>> GetAll(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        return await dbContext.Set<Pizza>()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PizzaResponse
            (
                p.Id,
                p.Name,
                p.Description,
                p.BasePrice,
                p.IsActive,
                p.PizzaToppings.Select(pt => pt.ToppingId).ToList()
            ))
            .ToListAsync();
    }

    public async Task<Result<PizzaResponse>> GetById(long id)
    {
        var pizza = await dbContext.Set<Pizza>()
            .Where(p => p.Id == id)
            .Select(p => new PizzaResponse
            (
                p.Id,
                p.Name,
                p.Description,
                p.BasePrice,
                p.IsActive,
                p.PizzaToppings.Select(pt => pt.ToppingId).ToList()
            ))
            .FirstOrDefaultAsync();

        if (pizza == null)
        {
            return Result.Failure(ErrorType.NotFound, $"Pizza with id {id} was not found.");
        }

        return pizza;
    }
}
