using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;

namespace PizzaCleanApp.Core.Features.Orders.AddOrderItem
{
    public class AddOrderItemUseCase(IDbContext dbContext)
    {
        public async Task<Result<AddOrderItemResponse>> Execute(AddOrderItemRequest request)
        {
            var order = await dbContext.Set<Order>()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
            {
                order = new Order { OrderDate = DateTime.UtcNow };
                dbContext.Set<Order>().Add(order);
            }

            var pizza = await dbContext.Set<Pizza>().Include(p => p.PizzaToppings).FirstOrDefaultAsync(p => p.Id == request.PizzaId);
            var size = await dbContext.Set<Size>().FindAsync(request.SizeId);
            var crust = await dbContext.Set<Crust>().FindAsync(request.CrustId);

            var item = new OrderItem
            {
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
                // Get toppings that are not already included in the pizza
                var toppingsNotInPizza = request.ToppingIds.Except(
                    pizza.PizzaToppings.Select(pt => pt.ToppingId));
                
                var toppings = await dbContext.Set<Topping>()
                    .Where(t => toppingsNotInPizza.Contains(t.Id))
                    .ToListAsync();

                item.OrderToppings.AddRange(
                    toppings.Select(t => new OrderItemToppings
                    {
                        ToppingId = t.Id,
                        Topping = t
                    })
                );
            }

            item.SubtotalPrice = item.CalculateSubtotalPrice();

            order.Items.Add(item);

            order.TotalPrice = order.GetTotalOrderPrice();

            await dbContext.SaveChangesAsync();

            return new AddOrderItemResponse(
                OrderId: order.Id,
                OrderDate: order.OrderDate,
                TotalPrice: order.TotalPrice
            );
        }
    }
}
