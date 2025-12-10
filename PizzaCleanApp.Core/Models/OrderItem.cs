namespace PizzaCleanApp.Core.Models;

public class OrderItem : BaseEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public long PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;
    public long SizeId { get; set; }
    public Size Size { get; set; } = null!;
    public long CrustId { get; set; }
    public Crust Crust { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal SubtotalPrice { get; set; }
}
