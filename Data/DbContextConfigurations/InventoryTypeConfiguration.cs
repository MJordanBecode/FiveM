using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class InventoryTypeConfiguration : IEntityTypeConfiguration<InventoryTypes>
{
    public void Configure(EntityTypeBuilder<InventoryTypes> builder)
    {
        builder.ToTable("InventoryTypes");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}