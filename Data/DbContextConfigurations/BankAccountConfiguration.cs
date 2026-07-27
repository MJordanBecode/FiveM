using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
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

            // Index unique pour garantir le 1:1 en BDD
            builder.HasIndex(b => b.PlayerID).IsUnique();

            // La relation 1:1 est déclarée ici de manière explicite
            builder.HasOne(b => b.Player)
                .WithOne(p => p.BankAccount)
                .HasForeignKey<BankAccounts>(b => b.PlayerID)
                .OnDelete(DeleteBehavior.Cascade); // Optionnel : supprime le compte si le joueur est supprimé
        }
    }
}