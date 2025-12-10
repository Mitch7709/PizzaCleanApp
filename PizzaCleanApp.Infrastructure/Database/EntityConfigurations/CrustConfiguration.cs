using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class CrustConfiguration : IEntityTypeConfiguration<Crust>
{
    public void Configure(EntityTypeBuilder<Crust> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(Crust.MaxLength.Name);
        builder.Property(c => c.Calories)
            .IsRequired();

        builder.HasData(
            new Crust { Id = 1, Name = "Thin Crust", Calories = 90, CreateDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Crust { Id = 2, Name = "Original Crust", Calories = 120, CreateDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Crust { Id = 3, Name = "Thick Crust", Calories = 150, CreateDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Crust { Id = 4, Name = "Stuffed Crust", Calories = 200, CreateDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow }
        );
    }
}
