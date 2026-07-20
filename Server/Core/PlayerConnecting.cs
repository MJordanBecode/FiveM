using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Lostgen.Server.Core
{
    public class PlayerConnecting : BaseScript
    {
        public PlayerConnecting()
        {
            Debug.WriteLine("PlayerConnecting enregistré et géré par FiveM.");
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
        }

        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic kickReason, dynamic deferrals)
        {
            deferrals.defer();
            await Delay(0); // On est sur le thread principal au départ

            deferrals.update($"Connexion en cours... Bonjour {playerName} !");

            if (ServerBootstrapper.ServiceProvider == null)
            {
                Debug.WriteLine($"[ERROR] ServiceProvider non initialisé au moment de la connexion de {playerName}.");
                deferrals.done("Erreur interne du serveur : le service de base de données n'est pas encore prêt.");
                return;
            }

            Debug.WriteLine($"===== IDENTIFIANTS DE {playerName} =====");
            foreach (var identifier in player.Identifiers)
            {
                Debug.WriteLine(identifier);
            }
            Debug.WriteLine("===========================");

            string rawLicense = player.Identifiers["license"];
            string rawSteam = player.Identifiers["steam"];
            string rawDiscord = player.Identifiers["discord"];

            if (string.IsNullOrEmpty(rawDiscord))
            {
                Debug.WriteLine($"[REFUS] {playerName} n'a pas de compte Discord lié.");
                deferrals.done("Vous devez avoir votre compte Discord lié à FiveM pour vous connecter.");
                return;
            }

            if (string.IsNullOrEmpty(rawLicense) || string.IsNullOrEmpty(rawSteam))
            {
                Debug.WriteLine($"[ERROR] Identifiants de jeu manquants pour {playerName} (license={rawLicense}, steam={rawSteam}).");
                deferrals.done("Impossible de récupérer vos licences de jeu (Rockstar/Steam).");
                return;
            }

            string cleanDiscordId = rawDiscord.Replace("discord:", "");
            string cleanLicense = rawLicense.Replace("license:", "");
            string cleanSteamHex = rawSteam.Replace("steam:", "");

            if (!ulong.TryParse(cleanDiscordId, out ulong discordIdLong))
            {
                Debug.WriteLine($"[ERROR] Impossible de parser l'ID Discord en ulong : {cleanDiscordId}");
                deferrals.done("Erreur de format sur votre identifiant Discord.");
                return;
            }

            bool isWhitelisted = false;
            string errorMessage = string.Empty;

            try
            {
                // L'appel asynchrone BDD se fait ici (changement de thread potentiel)
                using var scope = ServerBootstrapper.ServiceProvider.CreateScope();
                var playerService = scope.ServiceProvider.GetRequiredService<PlayerService>();

                var playerVm = await playerService.CreatePlayerAsync(cleanLicense, cleanSteamHex, discordIdLong);
                isWhitelisted = playerVm.IsWhitelisted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Échec de la connexion pour {playerName} : {ex}");
                errorMessage = "Une erreur technique est survenue lors de la vérification de votre profil.";
            }

            // 🔥 SÉCURITÉ CRITIQUE : On force le retour sur le Main Thread de FiveM avant de toucher à 'deferrals'
            await Delay(0);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                deferrals.done(errorMessage);
                return;
            }

            if (!isWhitelisted)
            {
                Debug.WriteLine($"[REFUS] {playerName} (Discord: {discordIdLong}) n'est pas whitelist.");
                deferrals.done("Vous n'êtes pas whitelist sur ce serveur.");
                return;
            }

            Debug.WriteLine($"[OK] Joueur {playerName} synchronisé en DB (license={cleanLicense}, discord={discordIdLong}).");

            // Appelé en toute sécurité sur le Main Thread
            deferrals.done();
        }
    }
}