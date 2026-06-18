using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransactions>
{
    public void Configure(EntityTypeBuilder<BankTransactions> builder)
    {
        builder.ToTable("BankTransactions");

        builder.HasKey(t => t.ID);

        builder.Property(t => t.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .HasMaxLength(255);

        builder.HasOne(t => t.BankAccount)
            .WithMany()
            .HasForeignKey(t => t.BankAccountID);
    }
}