using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class PizzaConfiguration: IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.ToTable("Pizzas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Pizza.MaxLength.Name);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Pizza.MaxLength.Description);

        builder.Property(p => p.BasePrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasData(

            new Pizza
            {
                Id = 1,
                Name = "Pepperoni",
                Description = "Classic pizza with pepperoni slices.",
                BasePrice = 8.00m,
                IsActive = true,
                CreateDate = new DateTime(2025, 12, 31, 2, 0, 0, DateTimeKind.Utc),
                LastUpdated = new DateTime(2025, 12, 31, 2, 0, 0, DateTimeKind.Utc)
            },
            new Pizza
            {
                Id = 2,
                Name = "Supreme",
                Description = "Deluxe pizza with a variety of toppings.",
                BasePrice = 12.50m,
                IsActive = true,
                CreateDate = new DateTime(2025, 12, 31, 2, 0, 0, DateTimeKind.Utc),
                LastUpdated = new DateTime(2025, 12, 31, 2, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
