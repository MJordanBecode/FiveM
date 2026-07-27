using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
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

            // Relation 1:1 avec Identifiers (Identifiers détient PlayerID)
            builder.HasOne(p => p.Identifier)
                .WithOne(i => i.Player)
                .HasForeignKey<Identifiers>(i => i.PlayerID);

            // Relation 1:1 avec PlayerSkins (Players détient SkinID)
            builder.HasOne(p => p.Skin)
                .WithOne(s => s.Player)
                .HasForeignKey<Players>(p => p.SkinID)
                .IsRequired(false); // Le skin peut être null au tout début avant la création du perso
        }
    }
}