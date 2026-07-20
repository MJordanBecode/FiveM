using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccounts>
{
    public void Configure(EntityTypeBuilder<BankAccounts> builder)
    {
        builder.ToTable("BankAccounts");

        builder.HasKey(b => b.ID);

        builder.Property(b => b.Pin)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(b => b.IsActived)
            .HasDefaultValue(true);

        builder.Property(b => b.Balance)
            .HasDefaultValue(0);

        builder.HasIndex(b => b.PlayerID).IsUnique();

        builder.HasOne(b => b.Player)
            .WithOne(p => p.BankAccount)
            .HasForeignKey<BankAccounts>(b => b.PlayerID);
    }
}