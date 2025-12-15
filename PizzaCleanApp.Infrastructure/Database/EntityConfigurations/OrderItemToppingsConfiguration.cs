using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class OrderItemToppingsConfiguration : IEntityTypeConfiguration<OrderItemToppings>
{
    public void Configure(EntityTypeBuilder<OrderItemToppings> builder)
    {
        builder.ToTable("OrderItemToppings");
        builder.HasKey(x => x.Id);

        // Deleting an OrderItem should delete its OrderItemToppings
        builder.HasOne(x => x.OrderItem)
            .WithMany()
            .HasForeignKey(x => x.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // Deleting a Topping should not affect OrderItemToppings
        builder.HasOne(x => x.Topping)
            .WithMany()
            .HasForeignKey(x => x.ToppingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
