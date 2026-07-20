using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting; // Permet de gérer le mode Dev/Prod
using System;
using System.IO;
using System.Linq;

namespace ConsoleFiveM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("  AUTO-MIGRATION AU DEMARRAGE");
            Console.WriteLine("========================================");

            try
            {
                ApplyPendingMigrations();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("ERREUR :");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("Appuie sur une touche pour continuer...");
            Console.ReadKey();

            // ... reste de ton programme (démarrage du serveur, etc.)
        }

        private static void ApplyPendingMigrations()
        {
            var connectionString = GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(
                connectionString,
                mysqlOptions => mysqlOptions.ServerVersion(new Version(8, 0, 21), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql)
            );

            using var context = new ApplicationDbContext(optionsBuilder.Options);

            // 💡 Astuce : On détecte si on est en Dev ou en Prod (via une variable d'environnement ou ton appsettings)
            // Pour tester en local, tu peux forcer à true. En prod, tu le mettras à false.
            bool isDevelopment = true;

            if (isDevelopment)
            {
                Console.WriteLine("MODE DEV : Wipe et recréation de la DB...");
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // ⚡ C'est ici que la magie de ton taff opère :
                Console.WriteLine("Génération des données de test (Seeding)...");
                //SeedDevData(context);
            }
            else
            {
                // ==========================================
                // 🚀 MODE PRODUCTION : PAS DE WIPE !
                // ==========================================
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("MODE PROD DETECTE : Analyse des migrations sans perte de données...");
                Console.ResetColor();

                var pendingMigrations = context.Database.GetPendingMigrations().ToList();

                if (pendingMigrations.Count == 0)
                {
                    Console.WriteLine("Aucune migration en attente. La prod est à jour.");
                    return;
                }

                Console.WriteLine($"Application de {pendingMigrations.Count} patch(es) de migration...");
                context.Database.Migrate(); // Applique uniquement les changements (ex: ajoute une colonne)

                // Remplissage de ton historique personnalisé
                var dynamicSet = context.Set<EFMigrationsDataHistory>();
                foreach (var migrationId in pendingMigrations)
                {
                    if (!dynamicSet.Any(m => m.MigrationId == migrationId))
                    {
                        dynamicSet.Add(new EFMigrationsDataHistory
                        {
                            MigrationId = migrationId,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Prod mise à jour avec succès et historique sauvegardé !");
                Console.ResetColor();
            }
        }

        //private static void SeedDevData(ApplicationDbContext context)
        //{
        //    // Si aucun joueur n'existe, on crée un faux joueur et un faux compte pour tester
        //    if (!context.Players.Any())
        //    {
        //        var testPlayer = new Players { Name = "Jordan Test", Identifier = "license:12345" };
        //        context.Players.Add(testPlayer);

        //        context.BankAccounts.Add(new BankAccounts
        //        {
        //            Player = testPlayer,
        //            Balance = 500000, // Directement riche pour tester les fonctionnalités !
        //            Pin = "1234"
        //        });

        //        context.SaveChanges();
        //        Console.WriteLine("✅ Données de test injectées avec succès !");
        //    }
        //} il faut créer une méthode qui va regrouper tous les les seeders pour les données de test, et l'appeler ici. 

        private static string GetConnectionString()
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Data");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(basePath))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string introuvable dans Data/appsettings.json");
        }


    }
}