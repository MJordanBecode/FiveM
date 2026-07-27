using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PlayerCharacterConfiguration : IEntityTypeConfiguration<PlayerCharacters>
    {
        public void Configure(EntityTypeBuilder<PlayerCharacters> builder)
        {
            builder.ToTable("PlayerCharacters");

            builder.HasIndex(x => x.PlayerID)
                .IsUnique();

            builder.HasKey(c => c.ID);


            builder.Property(c => c.FirstName)
                .HasMaxLength(50);


            builder.Property(c => c.LastName)
                .HasMaxLength(50);


            builder.Property(c => c.Gender)
                .HasMaxLength(1);


            builder.HasOne(c => c.Player)
                .WithMany(p => p.Characters)
                .HasForeignKey(c => c.PlayerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Skin)
                .WithOne(s => s.Character)
                .HasForeignKey<PlayerCharacters>(c => c.SkinID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}