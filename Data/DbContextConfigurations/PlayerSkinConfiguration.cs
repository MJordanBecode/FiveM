using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PlayerSkinConfiguration : IEntityTypeConfiguration<PlayerSkins>
{
    public void Configure(EntityTypeBuilder<PlayerSkins> builder)
    {
        builder.ToTable("PlayerSkins");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Face)
            .HasColumnType("json");

        builder.Property(x => x.Hair)
            .HasColumnType("json");

        builder.Property(x => x.Clothes)
            .HasColumnType("json");

        builder.Property(x => x.Props)
            .HasColumnType("json");

        builder.Property(x => x.Overlays)
            .HasColumnType("json");
    }
}