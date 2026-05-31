using ClassLibrary1.Models;
using ClassLibrary1.Models.pasImplemente;
using FivemCsharpCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Banque
    public DbSet<BankAccounts> BankAccounts => Set<BankAccounts>();
    public DbSet<BankTransactions> BankTransactions => Set<BankTransactions>();

    // Véhicules
    public DbSet<Vehicles> Vehicles => Set<Vehicles>();
    public DbSet<Vehiclecategories> VehicleCategories => Set<Vehiclecategories>();
    public DbSet<CarDealers> CarDealers => Set<CarDealers>();
    public DbSet<CarDealerVehicles> CarDealerVehicles => Set<CarDealerVehicles>();
    public DbSet<Garages> Garages => Set<Garages>();
    public DbSet<GarageCategories> GarageCategories => Set<GarageCategories>();

    // Inventaire
    public DbSet<Inventories> Inventories => Set<Inventories>();
    public DbSet<InventoryItems> InventoryItems => Set<InventoryItems>();
    public DbSet<InventoryTypes> InventoryTypes => Set<InventoryTypes>();
    public DbSet<Items> Items => Set<Items>();
    public DbSet<ItemCategories> ItemCategories => Set<ItemCategories>();
    public DbSet<ItemEffects> ItemEffects => Set<ItemEffects>();

    // Joueurs / métiers
    public DbSet<Players> Players => Set<Players>();
    public DbSet<RolePlayers> RolePlayers => Set<RolePlayers>();
    public DbSet<PlayerJobs> PlayerJobs => Set<PlayerJobs>();
    public DbSet<PlayerSkins> PlayerSkins => Set<PlayerSkins>();
    public DbSet<PlayerCharacters> PlayerCharacters => Set<PlayerCharacters>();
    public DbSet<PlayerVehicles> PlayerVehicles => Set<PlayerVehicles>();
    public DbSet<Jobs> Jobs => Set<Jobs>();
    public DbSet<JobGrades> JobGrades => Set<JobGrades>();

    // Permissions
    public DbSet<Roles> Roles => Set<Roles>();
    public DbSet<Permissions> Permissions => Set<Permissions>();
    public DbSet<RolePermissions> RolePermissions => Set<RolePermissions>();
}