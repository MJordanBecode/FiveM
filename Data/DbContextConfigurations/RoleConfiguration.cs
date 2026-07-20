using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.ID);

        builder.Property(r => r.RoleName)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(r => r.Label)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(r => r.RoleName).IsUnique();
    }
}