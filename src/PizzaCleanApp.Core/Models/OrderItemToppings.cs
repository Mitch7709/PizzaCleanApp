using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCleanApp.Core.Models;

public class OrderItemToppings
{
    public long OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = null!;
    public long ToppingId { get; set; }
    public Topping Topping { get; set; } = null!;
}
