using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Infrastructure.Database.EntityConfigurations;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.ToTable("Sizes");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(Size.MaxLength.Name);

        builder.Property(s => s.Price)
            .HasPrecision(18, 2);

        var currentTime = new DateTime(2025, 12, 21, 2, 0, 0, DateTimeKind.Utc);

        builder.HasData(
            new Size
            {
                Id = 1,
                Name = "Small",
                Price = 8.00m,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Size
            {
                Id = 2,
                Name = "Medium",
                Price = 10.00m,
                CreateDate = currentTime,
                LastUpdated = currentTime
            },
            new Size
            {
                Id = 3,
                Name = "Large",
                Price = 12.00m,
                CreateDate = currentTime,
                LastUpdated = currentTime
            }
        );
    }
}
