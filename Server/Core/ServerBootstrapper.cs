using System;
using CitizenFX.Core;
using Lostgen.Server.Controllers;
using Lostgen.Server.Interfaces;
using Lostgen.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Lostgen.Services;


namespace Lostgen.Server
{
    // BaseScript indique à FiveM que cette classe doit être démarrée automatiquement
    public class ServerBootstrapper : BaseScript
    {
        private readonly IServiceProvider _serviceProvider;

        public ServerBootstrapper()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            // On démarre nos contrôleurs (ils vont s'enregistrer auprès de FiveM)
            StartControllers();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 1. Enregistrement des Services
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IPlayerManager, PlayerManager>();

            // 2. Enregistrement des Contrôleurs (un contrôleur est aussi un service)
            services.AddSingleton<ConnectionController>();
        }

        private void StartControllers()
        {
            // En résolvant le ConnectionController, son constructeur est appelé,
            // ce qui va lui injecter automatiquement IApiService et IPlayerManager !
            _serviceProvider.GetRequiredService<ConnectionController>();

            Debug.WriteLine("[Framework] Initialisé avec succès !");
        }
    }
}