using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Players>
{
    public void Configure(EntityTypeBuilder<Players> builder)
    {
        builder.ToTable("Players");

        builder.HasKey(p => p.ID);

        builder.Property(p => p.FirstName).HasMaxLength(50);
        builder.Property(p => p.LastName).HasMaxLength(50);
        builder.Property(p => p.Gender).HasMaxLength(1);

        builder.Property(p => p.ConnectionOnce)
            .HasDefaultValue(false);

        builder.Property(p => p.IsWhitelisted)
            .HasDefaultValue(true);

        builder.HasOne(p => p.Identifier)
            .WithOne(i => i.Player)
            .HasForeignKey<Identifiers>(i => i.PlayerID);

        //builder.HasOne(p => p.BankAccount)
        //    .WithOne(b => b.Player)
        //    .HasForeignKey<BankAccounts>(b => b.PlayerID);
    }
}