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


    }
}
