using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class CarDealerConfiguration : IEntityTypeConfiguration<CarDealers>
{
    public void Configure(EntityTypeBuilder<CarDealers> builder)
    {
        builder.ToTable("CarDealers");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Location)
            .HasColumnType("json");

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}