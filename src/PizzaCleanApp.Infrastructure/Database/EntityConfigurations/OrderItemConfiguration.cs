using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.Id);

        // Deleting an Order should delete its OrderItems
        builder.HasOne(x => x.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Deleting OrderItem should not affect Pizza/Size/Crust
        builder.HasOne(x => x.Pizza)
            .WithMany()
            .HasForeignKey(x => x.PizzaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Size)
            .WithMany()
            .HasForeignKey(x => x.SizeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Crust)
            .WithMany()
            .HasForeignKey(x => x.CrustId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
