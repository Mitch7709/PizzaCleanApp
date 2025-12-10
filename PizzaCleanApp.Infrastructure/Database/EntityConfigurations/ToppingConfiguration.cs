using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class ToppingConfiguration : IEntityTypeConfiguration<Topping>
{
    public void Configure(EntityTypeBuilder<Topping> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(Topping.MaxLength.Name);

        builder.Property(t => t.Price)
            .HasPrecision(18, 2);

        builder.Property(t => t.Calories)
            .IsRequired();

        builder.Property(t => t.CategoryType)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .IsRequired();
    }
}
