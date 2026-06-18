using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicles>
{
    public void Configure(EntityTypeBuilder<Vehicles> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SpawnName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BasePrice)
            .HasDefaultValue(0);

        builder.HasIndex(x => x.SpawnName)
            .IsUnique();

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryID);
    }
}