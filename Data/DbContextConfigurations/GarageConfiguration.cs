using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class GarageConfiguration : IEntityTypeConfiguration<Garages>
{
    public void Configure(EntityTypeBuilder<Garages> builder)
    {
        builder.ToTable("Garages");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OwnerType)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.OwnerID)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Position)
            .HasColumnType("json");

        builder.HasIndex(x => new { x.OwnerType, x.OwnerID });

        builder.HasOne(x => x.GarageCategory)
            .WithMany()
            .HasForeignKey(x => x.GarageCategoryID);
    }
}