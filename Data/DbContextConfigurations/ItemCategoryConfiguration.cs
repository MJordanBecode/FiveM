using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategories>
{
    public void Configure(EntityTypeBuilder<ItemCategories> builder)
    {
        builder.ToTable("ItemCategories");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Label).HasMaxLength(60);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}