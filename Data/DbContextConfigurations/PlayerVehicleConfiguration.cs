using ClassLibrary1.Models.pasImplemente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PlayerVehicleConfiguration : IEntityTypeConfiguration<PlayerVehicles>
{
    public void Configure(EntityTypeBuilder<PlayerVehicles> builder)
    {
        builder.ToTable("PlayerVehicles");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Plate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.VehicleProps)
            .HasColumnType("json");

        builder.HasIndex(x => x.Plate)
            .IsUnique();

        builder.HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleID);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerID);
    }
}