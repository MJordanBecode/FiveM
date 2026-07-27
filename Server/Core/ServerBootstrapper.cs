using CitizenFX.Core;
using Data.Context;
using Data.Repositories;
using Lostgen.Server.Managers;
using Lostgen.Server.Services;
using Lostgen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces; // Contient ICharacterService, IPlayerService, IApiService, etc.
using Services.Services;   // Contient CharacterService, PlayerService, ApiService, etc.
using System;
using System.IO;

namespace Lostgen.Server
{
    public class ServerBootstrapper : BaseScript
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public ServerBootstrapper()
        {
            Debug.WriteLine("===== LOSTGEN SERVER DEMARRE =====");

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            StartControllers();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Base de données
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = "server=localhost;database=lostgenrp;user=root;password=";

                options.UseMySql(
                    connectionString,
                    mysqlOptions => mysqlOptions.ServerVersion(
                        new Version(8, 0, 21),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                );
            });

            // Repositories
            services.AddScoped<PlayerRepository>();

            // Services métiers
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IPlayerManager, PlayerManager>();
            services.AddScoped<ICharacterService, CharacterService>();
        }

        private void StartControllers()
        {
            if (ServiceProvider == null)
            {
                Debug.WriteLine("ServiceProvider est NULL !");
                return;
            }

            // Test EF Core & Génération du script SQL
            try
            {
                using var scope = ServiceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string sqlScript = context.Database.GenerateCreateScript();

                File.WriteAllText(@"C:\Users\jorda\Desktop\init.sql", sqlScript);
                Debug.WriteLine("ApplicationDbContext initialisé.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur EF Core : {ex}");
            }

            Debug.WriteLine("===== INITIALISATION TERMINEE =====");
        }
    }
}