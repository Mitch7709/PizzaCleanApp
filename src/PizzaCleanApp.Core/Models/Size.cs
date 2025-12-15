namespace PizzaCleanApp.Core.Models;

public class Size : BaseEntity
{
    public static class MaxLength
    {
        public const int Name = 100;
    }
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
