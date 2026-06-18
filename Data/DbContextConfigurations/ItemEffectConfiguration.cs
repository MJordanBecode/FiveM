using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class ItemEffectConfiguration : IEntityTypeConfiguration<ItemEffects>
{
    public void Configure(EntityTypeBuilder<ItemEffects> builder)
    {
        builder.ToTable("ItemEffects");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.EffectType)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemID);
    }
}