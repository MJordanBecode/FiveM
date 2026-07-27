using CitizenFX.Core;
using CitizenFX.Core.Native;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using System;

namespace Lostgen.Server.Core
{
    public class PlayerLoadedServer : BaseScript
    {
        public PlayerLoadedServer()
        {
            EventHandlers["lostgen:server:playerReady"] += new Action<Player>(OnPlayerReady);
        }

        private async void OnPlayerReady([FromSource] Player player)
        {
            if (player == null) return;

            string rawLicense = player.Identifiers["license"];
            if (string.IsNullOrEmpty(rawLicense)) return;

            string cleanLicense = rawLicense.Replace("license:", "");

            try
            {
                if (ServerBootstrapper.ServiceProvider == null) return;

                using var scope = ServerBootstrapper.ServiceProvider.CreateScope();
                var playerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();

                // On récupère le joueur en BDD
                var playerDb = await playerService.GetPlayerByLicenseAsync(cleanLicense);

                if (playerDb == null) return;

                // 🔥 Force le retour sur le Main Thread FiveM
                await Delay(0);

                if (!playerDb.ConnectionOnce)
                {
                    // 🔴 PREMIÈRE CONNEXION : On l'isole et on lance la création

                    // 1. On applique le Routing Bucket avec player.Handle directement (string)
                    if (int.TryParse(player.Handle, out int playerServerId))
                    {
                        int privateBucket = 1000 + playerServerId;
                        // API.SetPlayerRoutingBucket attend (string playerId, int bucketId)
                        API.SetPlayerRoutingBucket(player.Handle, privateBucket);
                    }

                    // 2. On envoie l'événement au client via l'objet player directement
                    // Pour éviter les conflits de types, on utilise la méthode native d'envoi d'événement :
                    Debug.WriteLine("Envoi de l'événement au client");
                    TriggerClientEvent(player, "lostgen:client:startCharacterCreation", playerDb.ID.ToString());
                }
                else
                {
                    // 🟢 DÉJÀ CRÉÉ
                    TriggerClientEvent(player, "lostgen:client:loadCharacterSkin", playerDb.Skin);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Erreur lors du chargement de {player.Name} : {ex}");
            }
        }
    }
}