using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class PizzaToppingConfiguration : IEntityTypeConfiguration<PizzaTopping>
{
    public void Configure(EntityTypeBuilder<PizzaTopping> builder)
    {
        builder.ToTable("PizzaToppings");
        // Composite primary key ensures uniqueness of each Pizza-Topping pair
        builder.HasKey(pt => new { pt.PizzaId, pt.ToppingId });

        // Relationship to Pizza (one Pizza has many PizzaToppings)
        builder.HasOne(pt => pt.Pizza)
            .WithMany(p => p.PizzaToppings)
            .HasForeignKey(pt => pt.PizzaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship to Topping (one Topping can be used on many Pizzas)
        builder.HasOne(pt => pt.Topping)
            .WithMany()
            .HasForeignKey(pt => pt.ToppingId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // Optional: index to speed up lookups by Pizza
        builder.HasIndex(pt => pt.PizzaId);

        // Optional: index to speed up lookups by Topping
        builder.HasIndex(pt => pt.ToppingId);

        builder.HasData(
            new PizzaTopping
            {
                PizzaId = 1,
                ToppingId = 1 // Pepperoni
            },
            new PizzaTopping
            {
                PizzaId = 2,
                ToppingId = 1 
            },
            new PizzaTopping
            {
                PizzaId = 2,
                ToppingId = 2 // Meatballs
            },
            new PizzaTopping
            {
                PizzaId = 2,
                ToppingId = 4 // Mushrooms
            },
            new PizzaTopping
            {
                PizzaId = 2,
                ToppingId = 5 // Onions
            },
            new PizzaTopping
            {
                PizzaId = 2,
                ToppingId = 6 // Green Peppers
            }
        );
    }
}
