using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Jobs>
{
    public void Configure(EntityTypeBuilder<Jobs> builder)
    {
        builder.ToTable("Jobs");

        builder.HasKey(j => j.ID);

        builder.Property(j => j.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(j => j.Logo)
            .HasMaxLength(255);

        builder.HasIndex(j => j.Name).IsUnique();
    }
}