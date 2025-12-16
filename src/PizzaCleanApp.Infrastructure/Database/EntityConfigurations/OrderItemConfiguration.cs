using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.SubtotalPrice)
               .HasPrecision(18, 2);

        builder.Property(oi => oi.Quantity)
               .IsRequired();

        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.Items)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Pizza)
               .WithMany()
               .HasForeignKey(oi => oi.PizzaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oi => oi.Size)
               .WithMany()
               .HasForeignKey(oi => oi.SizeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oi => oi.Crust)
               .WithMany()
               .HasForeignKey(oi => oi.CrustId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
