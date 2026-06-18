using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PlayerCharacterConfiguration : IEntityTypeConfiguration<PlayerCharacters>
{
    public void Configure(EntityTypeBuilder<PlayerCharacters> builder)
    {
        builder.ToTable("PlayerCharacters");

        builder.HasKey(x => x.ID);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerID);

        builder.HasOne(x => x.PlayerSkin)
            .WithMany()
            .HasForeignKey(x => x.PlayerSkinID);

        builder.HasIndex(x => new
        {
            x.PlayerID,
            x.PlayerSkinID
        });
    }
}