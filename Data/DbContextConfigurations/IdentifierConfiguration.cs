using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class IdentifierConfiguration : IEntityTypeConfiguration<Identifiers>
{
    public void Configure(EntityTypeBuilder<Identifiers> builder)
    {
        builder.ToTable("Identifiers");

        builder.HasKey(i => i.ID);

        builder.Property(i => i.DiscordLicense)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(i => i.FiveMLicense)
            .HasMaxLength(64);

        builder.Property(i => i.SteamLicense)
            .HasMaxLength(64);

        builder.HasIndex(i => i.PlayerID).IsUnique();
        builder.HasIndex(i => i.DiscordLicense).IsUnique();
        builder.HasIndex(i => i.FiveMLicense).IsUnique();
        builder.HasIndex(i => i.SteamLicense).IsUnique();
    }
}