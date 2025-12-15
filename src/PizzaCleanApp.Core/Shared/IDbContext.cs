using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Shared
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        Task<int> SaveChangesAsync();
    }
}
