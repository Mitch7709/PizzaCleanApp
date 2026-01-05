using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Orders.AddOrderItem
{
    public class AddOrderItemUseCase(IDbContext dbContext)
    {
        public async Task Execute(int orderId, AddOrderItemRequest request)
        {
            var order = await dbContext.Set<Order>()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                order = new Order { Id = orderId, OrderDate = DateTime.UtcNow };
                dbContext.Set<Order>().Add(order);
            }

            var pizza = await dbContext.Set<Pizza>().FindAsync(request.PizzaId);
            var size = await dbContext.Set<Size>().FindAsync(request.SizeId);
            var crust = await dbContext.Set<Crust>().FindAsync(request.CrustId);

            if (pizza is null) throw new InvalidOperationException($"Pizza {request.PizzaId} not found.");
            if (size is null) throw new InvalidOperationException($"Size {request.SizeId} not found.");
            if (crust is null) throw new InvalidOperationException($"Crust {request.CrustId} not found.");


            var item = new OrderItem
            {
                OrderId = order.Id,
                PizzaId = request.PizzaId,
                Pizza = pizza,
                Quantity = request.Quantity,
                SizeId = request.SizeId,
                Size = size,
                CrustId = request.CrustId,
                Crust = crust
            };

            if (request.ToppingIds is not null && request.ToppingIds.Count > 0)
            {
                var toppings = await dbContext.Set<Topping>()
                    .Where(t => request.ToppingIds.Contains(t.Id))
                    .ToListAsync();

                foreach (var topping in toppings)
                {
                    item.Toppings.Add(new OrderItemToppings
                    {
                        OrderItemId = item.Id,
                        ToppingId = topping.Id,
                        Topping = topping
                    });
                }
            }

            item.SubtotalPrice = item.CalculateSubtotalPrice();

            order.Items.Add(item);

            order.TotalPrice = order.GetTotalOrderPrice();

            await dbContext.SaveChangesAsync();
        }
    }
}
