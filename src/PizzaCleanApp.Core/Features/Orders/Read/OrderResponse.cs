using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Orders.Read
{
    public record OrderResponse(
        long Id,
        OrderStatus Status,
        decimal TotalPrice,
        DateTime OrderDate,
        IReadOnlyCollection<OrderItemResponse> Items
    );

    public record OrderItemResponse(
        long Id,
        string Pizza,
        int Quantity,
        string Size,
        string Crust,
        IReadOnlyCollection<string> Toppings
    );
}
