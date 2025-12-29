using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class ToppingConfiguration : IEntityTypeConfiguration<Topping>
{
    public void Configure(EntityTypeBuilder<Topping> builder)
    {
        builder.ToTable("Toppings");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(Topping.MaxLength.Name);

        builder.Property(t => t.Price)
            .HasPrecision(18, 2);

        builder.Property(t => t.Calories)
            .IsRequired();

        builder.Property(t => t.CategoryType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(t => t.IsActive)
            .IsRequired();

        var currentTime = new DateTime(2025, 12, 11, 2, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Topping
            {
                Id = 1,
                Name = "Pepperoni",
                Price = 1.50m,
                Calories = 54,
                CategoryType = ToppingCategory.Meat,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 2,
                Name = "Meatballs",
                Price = 2.00m,
                Calories = 61,
                CategoryType = ToppingCategory.Meat,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 3,
                Name = "Steak",
                Price = 1.75m,
                Calories = 25,
                CategoryType = ToppingCategory.Meat,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 4,
                Name = "Mushrooms",
                Price = 1.00m,
                Calories = 3,
                CategoryType = ToppingCategory.Vegetable,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 5,
                Name = "Onions",
                Price = 0.75m,
                Calories = 5,
                CategoryType = ToppingCategory.Vegetable,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 6,
                Name = "Green Peppers",
                Price = 0.80m,
                Calories = 4,
                CategoryType = ToppingCategory.Vegetable,
                IsActive = true,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Topping
            {
                Id = 7,
                Name = "Pineapple",
                Price = 1.20m,
                Calories = 10,
                CategoryType = ToppingCategory.Vegetable,
                IsActive = false,
                CreateDate = currentTime,
                LastUpdated = currentTime
            }
        );
    }
}
