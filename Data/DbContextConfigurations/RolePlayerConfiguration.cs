using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class RolePlayerConfiguration : IEntityTypeConfiguration<RolePlayers>
{
    public void Configure(EntityTypeBuilder<RolePlayers> builder)
    {
        builder.ToTable("RolePlayers");

        builder.HasKey(x => x.ID);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleID);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerID);

        builder.HasIndex(x => new
        {
            x.RoleID,
            x.PlayerID
        }).IsUnique();
    }
}