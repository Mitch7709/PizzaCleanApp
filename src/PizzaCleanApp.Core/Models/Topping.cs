using System.Text.Json.Serialization;

namespace PizzaCleanApp.Core.Models;

public class Topping : BaseEntity
{
    public static class MaxLength
    {
        public const int Name = 100;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Calories { get; set; }
    public ToppingCategory CategoryType { get; set; }
    public bool IsActive { get; set; } = true;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ToppingCategory
{
    Vegetable,
    Meat
}
