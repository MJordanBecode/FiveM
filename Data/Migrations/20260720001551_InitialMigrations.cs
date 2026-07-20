using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDealers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EFMigrationsDataHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MigrationId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFMigrationsDataHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarageCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MaxSlots = table.Column<short>(nullable: false),
                    MinSlots = table.Column<short>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    VehicleType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    BaseWeight = table.Column<int>(nullable: false),
                    MaxSlots = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Logo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    PermissionName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Height = table.Column<short>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    ConnectionOnce = table.Column<bool>(nullable: false),
                    IsWhitelisted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSkins",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Face = table.Column<string>(nullable: false),
                    Hair = table.Column<string>(nullable: false),
                    Clothes = table.Column<string>(nullable: false),
                    Props = table.Column<string>(nullable: true),
                    Overlays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerType = table.Column<string>(nullable: false),
                    OwnerID = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    GarageCategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Garages_GarageCategories_GarageCategoryID",
                        column: x => x.GarageCategoryID,
                        principalTable: "GarageCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    OwnerID = table.Column<string>(nullable: false),
                    OwnerType = table.Column<string>(nullable: false),
                    BonusWeight = table.Column<short>(nullable: false),
                    BonusSlots = table.Column<short>(nullable: false),
                    InventoryName = table.Column<string>(nullable: false),
                    InventoryTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventories_InventoryTypes_InventoryTypeID",
                        column: x => x.InventoryTypeID,
                        principalTable: "InventoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Label = table.Column<string>(maxLength: 30, nullable: false),
                    Weight = table.Column<short>(nullable: false),
                    Stackable = table.Column<bool>(nullable: false),
                    MaxStack = table.Column<short>(nullable: false),
                    MinStack = table.Column<short>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ItemCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    TEEEEST = table.Column<string>(nullable: false),
                    Pin = table.Column<string>(nullable: false),
                    IsActived = table.Column<bool>(nullable: false),
                    Balance = table.Column<long>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identifiers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    FiveMLicense = table.Column<string>(nullable: true),
                    DiscordLicense = table.Column<ulong>(nullable: false),
                    SteamLicense = table.Column<string>(nullable: true),
                    PlayerID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifiers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Identifiers_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCharacters",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false),
                    PlayerSkinID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCharacters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_PlayerSkins_PlayerSkinID",
                        column: x => x.PlayerSkinID,
                        principalTable: "PlayerSkins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobGrades",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<short>(nullable: false),
                    JobID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobGrades_Jobs_JobID",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobGrades_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    RolesID = table.Column<Guid>(nullable: false),
                    PermissionsID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RolesID",
                        column: x => x.RolesID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePlayers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePlayers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePlayers_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePlayers_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SpawnName = table.Column<string>(nullable: false),
                    BasePrice = table.Column<long>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "VehicleCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Quantity = table.Column<short>(nullable: false),
                    Durability = table.Column<byte>(nullable: false),
                    Metadata = table.Column<string>(nullable: false),
                    Slots = table.Column<short>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false),
                    InventoryID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemEffects",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    EffectType = table.Column<string>(nullable: false),
                    ValueType = table.Column<short>(nullable: false),
                    ItemID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEffects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemEffects_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Amount = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    BankAccountID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankTransactions_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerJobs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false),
                    JobID = table.Column<Guid>(nullable: false),
                    JobGradeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerJobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerJobs_JobGrades_JobGradeID",
                        column: x => x.JobGradeID,
                        principalTable: "JobGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerJobs_Jobs_JobID",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerJobs_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarDealerVehicles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    Stock = table.Column<short>(nullable: false),
                    VehicleID = table.Column<Guid>(nullable: false),
                    CarDealerID = table.Column<Guid>(nullable: false),
                    GarageID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealerVehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarDealerVehicles_CarDealers_CarDealerID",
                        column: x => x.CarDealerID,
                        principalTable: "CarDealers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarDealerVehicles_Garages_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarDealerVehicles_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerVehicles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    Plate = table.Column<string>(nullable: false),
                    Fuel = table.Column<short>(nullable: false),
                    EngineHealth = table.Column<short>(nullable: false),
                    BodyHealth = table.Column<short>(nullable: false),
                    State = table.Column<short>(nullable: false),
                    VehicleProps = table.Column<string>(nullable: true),
                    VehicleID = table.Column<Guid>(nullable: false),
                    PlayerID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerVehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerVehicles_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerVehicles_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_PlayerID",
                table: "BankAccounts",
                column: "PlayerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_BankAccountID",
                table: "BankTransactions",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CarDealerVehicles_CarDealerID",
                table: "CarDealerVehicles",
                column: "CarDealerID");

            migrationBuilder.CreateIndex(
                name: "IX_CarDealerVehicles_GarageID",
                table: "CarDealerVehicles",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_CarDealerVehicles_VehicleID",
                table: "CarDealerVehicles",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Garages_GarageCategoryID",
                table: "Garages",
                column: "GarageCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Identifiers_PlayerID",
                table: "Identifiers",
                column: "PlayerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_InventoryTypeID",
                table: "Inventories",
                column: "InventoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_InventoryID",
                table: "InventoryItems",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemID",
                table: "InventoryItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_PlayerID",
                table: "InventoryItems",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEffects_ItemID",
                table: "ItemEffects",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryID",
                table: "Items",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_JobID",
                table: "JobGrades",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_RoleID",
                table: "JobGrades",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_PlayerID",
                table: "PlayerCharacters",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_PlayerSkinID",
                table: "PlayerCharacters",
                column: "PlayerSkinID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJobs_JobGradeID",
                table: "PlayerJobs",
                column: "JobGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJobs_JobID",
                table: "PlayerJobs",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJobs_PlayerID",
                table: "PlayerJobs",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerVehicles_PlayerID",
                table: "PlayerVehicles",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerVehicles_VehicleID",
                table: "PlayerVehicles",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionsID",
                table: "RolePermissions",
                column: "PermissionsID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RolesID",
                table: "RolePermissions",
                column: "RolesID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePlayers_PlayerID",
                table: "RolePlayers",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePlayers_RoleID",
                table: "RolePlayers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CategoryID",
                table: "Vehicles",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransactions");

            migrationBuilder.DropTable(
                name: "CarDealerVehicles");

            migrationBuilder.DropTable(
                name: "EFMigrationsDataHistories");

            migrationBuilder.DropTable(
                name: "Identifiers");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ItemEffects");

            migrationBuilder.DropTable(
                name: "PlayerCharacters");

            migrationBuilder.DropTable(
                name: "PlayerJobs");

            migrationBuilder.DropTable(
                name: "PlayerVehicles");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "RolePlayers");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CarDealers");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PlayerSkins");

            migrationBuilder.DropTable(
                name: "JobGrades");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "GarageCategories");

            migrationBuilder.DropTable(
                name: "InventoryTypes");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}
