using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permissions>
{
    public void Configure(EntityTypeBuilder<Permissions> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.ID);

        builder.Property(p => p.PermissionName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.PermissionName).IsUnique();
    }
}