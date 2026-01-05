using System.Text.Json.Serialization;

namespace PizzaCleanApp.Core.Models;

public class Order : BaseEntity
{
    public long Id { get; set; }

    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal GetTotalOrderPrice()
    {
        return Items.Sum(item => item.GetTotalPrice());
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}
