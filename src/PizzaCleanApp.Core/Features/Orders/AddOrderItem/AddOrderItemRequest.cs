using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Orders.AddOrderItem
{
    public record AddOrderItemRequest(
        long OrderId,
        long PizzaId,
        int Quantity,
        long SizeId,
        long CrustId,
        IReadOnlyCollection<long>? ToppingIds = null
    );
}
