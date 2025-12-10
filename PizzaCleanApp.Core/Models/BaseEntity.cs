namespace PizzaCleanApp.Core.Models;

public interface IEntity
{
}

public abstract class BaseEntity : IEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdated { get; set; }
}
