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


            builder.Property(p => p.ConnectionOnce)
                .HasDefaultValue(false);


            builder.Property(p => p.IsWhitelisted)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Identifier)
                .WithOne(i => i.Player)
                .HasForeignKey<Identifiers>(i => i.PlayerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Characters)
                .WithOne(c => c.Player)
                .HasForeignKey(c => c.PlayerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}