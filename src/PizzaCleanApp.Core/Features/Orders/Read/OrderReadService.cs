using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Orders.Read
{
    public class OrderReadService(IDbContext dbContext)
    {
        public async Task<Result<OrderResponse>> GetOrder(int orderId)
        {
            var order = await dbContext.Set<Order>()
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return Result.Failure(ErrorType.NotFound, "Order not found");

            var orderItems = await dbContext.Set<OrderItem>()
                .Include(i => i.Pizza)
                .Include(i => i.Size)
                .Include(i => i.Crust)
                .Include(i => i.Toppings)
                .Where(i => i.OrderId == orderId)
                .ToListAsync();

            // Map OrderItem to OrderItemResponse
            var itemResponses = orderItems
                .Select(item => new OrderItemResponse(
                    item.Id,
                    item.Pizza.Name,
                    item.Quantity,
                    item.Size.Name,
                    item.Crust.Name,
                    item.Toppings.Select(t => t.Topping.Name).ToList()
                    // Add other properties as needed
                ))
                .ToList()
                .AsReadOnly();

            var orderResponse = new OrderResponse(
                order.Id,
                order.Status,
                order.TotalPrice,
                order.OrderDate,
                itemResponses
            );

            return orderResponse;
        }
    }
}
