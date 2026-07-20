using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventories>
{
    public void Configure(EntityTypeBuilder<Inventories> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.OwnerID).IsRequired().HasMaxLength(64);
        builder.Property(x => x.OwnerType).IsRequired().HasMaxLength(30);
        builder.Property(x => x.InventoryName).IsRequired().HasMaxLength(50);

        builder.Property(x => x.BonusWeight).HasDefaultValue((short)0);
        builder.Property(x => x.BonusSlots).HasDefaultValue((short)0);

        builder.HasIndex(x => new { x.OwnerType, x.OwnerID });

        builder.HasOne(x => x.InventoryType)
            .WithMany()
            .HasForeignKey(x => x.InventoryTypeID);
    }
}