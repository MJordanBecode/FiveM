using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItems>
{
    public void Configure(EntityTypeBuilder<InventoryItems> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Durability).HasDefaultValue((byte)100);
        builder.Property(x => x.Metadata).HasColumnType("json");

        builder.HasOne(x => x.Inventory)
            .WithMany()
            .HasForeignKey(x => x.InventoryID);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerID);

        builder.HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemID);
    }
}