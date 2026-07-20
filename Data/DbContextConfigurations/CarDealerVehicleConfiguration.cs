using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class CarDealerVehicleConfiguration : IEntityTypeConfiguration<CarDealerVehicles>
{
    public void Configure(EntityTypeBuilder<CarDealerVehicles> builder)
    {
        builder.ToTable("CarDealerVehicles");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.Stock)
            .HasDefaultValue((short)0);

        builder.HasIndex(x => new { x.CarDealerID, x.VehicleID })
            .IsUnique();

        builder.HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleID);

        builder.HasOne(x => x.CarDealer)
            .WithMany()
            .HasForeignKey(x => x.CarDealerID);

        builder.HasOne(x => x.Garage)
            .WithMany()
            .HasForeignKey(x => x.GarageID);
    }
}