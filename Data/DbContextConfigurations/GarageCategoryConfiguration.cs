using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class GarageCategoryConfiguration : IEntityTypeConfiguration<GarageCategories>
{
    public void Configure(EntityTypeBuilder<GarageCategories> builder)
    {
        builder.ToTable("GarageCategories");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.VehicleType)
            .HasMaxLength(255);

        builder.Property(x => x.MaxSlots)
            .HasDefaultValue((short)1);

        builder.Property(x => x.MinSlots)
            .HasDefaultValue((short)0);

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}