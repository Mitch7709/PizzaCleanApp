using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Shared;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

    Task<int> SaveChangesAsync();


}
