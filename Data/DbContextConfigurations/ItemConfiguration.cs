using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Items>
{
    public void Configure(EntityTypeBuilder<Items> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Label).IsRequired().HasMaxLength(60);
        builder.Property(x => x.Description).HasMaxLength(255);

        builder.Property(x => x.MinStack).HasDefaultValue((short)1);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryID);
    }
}