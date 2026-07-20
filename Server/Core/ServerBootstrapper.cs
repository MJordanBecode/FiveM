using CitizenFX.Core;
using CitizenFX.Core.Native;
using Data.Context;
using Data.Repositories;
using Lostgen.Server.Controllers;
using Lostgen.Server.Managers;
using Lostgen.Server.Services;
using Lostgen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using System;
using System.IO;



namespace Lostgen.Server
{
    // BaseScript indique à FiveM que cette classe doit être démarrée automatiquement
    public class ServerBootstrapper : BaseScript
    {
        private readonly IServiceProvider _serviceProvider;
        public static IServiceProvider? ServiceProvider { get; private set; }
        public ServerBootstrapper()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            StartControllers();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 1. Configuration de la Base de Données (EF Core 3.1)
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Récupère ta chaîne de connexion (soit en dur pour le test, soit via un fichier config)
                string connectionString = "server=localhost;database=lostgen;user=root;password="; // à changer pour passer avec le JSON ! 

                options.UseMySql(
                    connectionString,
                    mysqlOptions => mysqlOptions.ServerVersion(new Version(8, 0, 21), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql)
                );
            });

            // 2. Enregistrement des Repositories et Services
            services.AddSingleton<PlayerRepository>();
            services.AddSingleton<PlayerService>();

            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IPlayerManager, PlayerManager>();

            // 3. Enregistrement des Contrôleurs
            services.AddSingleton<ConnectionController>();
        }

        private void StartControllers()
        {
            // TEST 1 : On écrit le fichier de test AVANT toute logique de base de données
            try
            {
                string testPath = @"C:\Users\jorda\Desktop\test_fivem.txt";
                File.WriteAllText(testPath, "Le C# de FiveM est bien vivant et s'exécute !");
            }
            catch (Exception testEx)
            {
                // Si Windows bloque les droits d'écriture sur le bureau
            }

            if (ServiceProvider != null)
            {
                // TEST 2 : On isole EF Core pour voir s'il est la cause du crash
                try
                {
                    using (var scope = ServiceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        // On génère le script SQL
                        string sqlScript = context.Database.GenerateCreateScript();

                        // Si ça arrive ici, magique, on écrit le .sql
                        string sqlPath = @"C:\Users\jorda\Desktop\init.sql";
                        File.WriteAllText(sqlPath, sqlScript);
                    }
                }
                catch (Exception ex)
                {
                    // Si MySQL ou EF Core crash, on capture l'erreur ici sans tuer le script
                    try
                    {
                        File.WriteAllText(@"C:\Users\jorda\Desktop\erreur_db.txt", ex.ToString());
                    }
                    catch { }
                }

                try
                {
                    ServiceProvider.GetRequiredService<ConnectionController>();
                }
                catch { }
            }
        }
    }
}