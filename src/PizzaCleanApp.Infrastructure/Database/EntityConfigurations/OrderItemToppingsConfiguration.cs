using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class OrderItemToppingsConfiguration : IEntityTypeConfiguration<OrderItemToppings>
{
    public void Configure(EntityTypeBuilder<OrderItemToppings> builder)
    {
        builder.ToTable("OrderItemToppings");
        builder.HasKey(oit => oit.Id);

        builder.Property(oit => oit.OrderItemId).IsRequired();
        builder.Property(oit => oit.ToppingId).IsRequired();

        builder.HasOne(oit => oit.OrderItem)
               .WithMany(oi => oi.Toppings)
               .HasForeignKey(oit => oit.OrderItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oit => oit.Topping)
               .WithMany()
               .HasForeignKey(oit => oit.ToppingId)
               .OnDelete(DeleteBehavior.Restrict);

        // Optional: prevent duplicate toppings per order item
        builder.HasIndex(oit => new { oit.OrderItemId, oit.ToppingId }).IsUnique();
    }
}
