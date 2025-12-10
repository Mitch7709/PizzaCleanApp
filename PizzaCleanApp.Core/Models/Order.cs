using System.Text.Json.Serialization;

namespace PizzaCleanApp.Core.Models;

public class Order : BaseEntity
{
    public long Id { get; set; }

    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalPrice { get; set; } = 0;
    public DateTime OrderDate { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}
