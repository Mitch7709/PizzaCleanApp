namespace PizzaCleanApp.Core.Models;

public class Pizza : BaseEntity
{
    public static class MaxLength
    {
        public const int Name = 100;
        public const int Description = 500;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; }

    public ICollection<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
}
