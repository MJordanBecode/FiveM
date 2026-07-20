using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Services.Services;
using Microsoft.Extensions.DependencyInjection; // Ne pas oublier pour GetService

namespace Lostgen.Server.Core
{
    public class PlayerConnecting : BaseScript
    {
        // 1. Le constructeur : On s'abonne JUSTE à l'événement
        public PlayerConnecting()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }

        // 2. La méthode de connexion
        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic kickReason, dynamic deferrals)
        {
            deferrals.defer();
            await BaseScript.Delay(0);

            deferrals.update($"Connexion en cours... Bonjour {playerName} !");

            // AJUSTEMENT COHÉRENCE : On récupère le service ICI, à la demande !
            PlayerService? playerService = null;
            if (ServerBootstrapper.ServiceProvider != null)
            {
                playerService = ServerBootstrapper.ServiceProvider.GetService<PlayerService>();
            }

            // Sécurité si le conteneur ou le service n'est pas prêt
            if (playerService == null)
            {
                Debug.WriteLine($"[ERROR] PlayerService introuvable au moment de la connexion de {playerName}.");
                deferrals.done("Erreur interne du serveur : Le service de base de données n'est pas encore prêt.");
                return;
            }

            // Récupération des identifiants
            string license = player.Identifiers["license"];
            string steamHex = player.Identifiers["steam"];
            string discordId = player.Identifiers["discord"];

            if (string.IsNullOrEmpty(license) || string.IsNullOrEmpty(steamHex) || string.IsNullOrEmpty(discordId))
            {
                deferrals.done("Identifiants de connexion manquants (Rockstar, Steam ou Discord). Connexion refusée.");
                return;
            }

            try
            {
                // Utilisation du service récupéré localement
                var playerVm = await playerService.CreatePlayerAsync(license, steamHex, discordId, playerName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Échec de la connexion pour {playerName} : {ex.Message}");
                deferrals.done("Une erreur technique est survenue lors de la vérification de votre profil.");
                return;
            }

            deferrals.done();
        }
    }
}