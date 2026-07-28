using CitizenFX.Core;
using Lostgen.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using System;

namespace Lostgen.Server.Core
{
    public class PlayerDropped : BaseScript
    {
        public PlayerDropped()
        {
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
        }

        private async void OnPlayerDropped([FromSource] Player player, string reason)
        {
            if (player == null)
                return;

            string rawLicense = player.Identifiers["license"];

            if (string.IsNullOrEmpty(rawLicense))
                return;

            string cleanLicense = rawLicense.Replace("license:", "");

            try
            {
                if (ServerBootstrapper.ServiceProvider == null)
                    return;

                // Récupération de la position AVANT que le ped ne disparaisse
                var pedEntity = player.Character;

                if (pedEntity == null)
                {
                    Debug.WriteLine("[PlayerDropped] Aucun ped associé, sauvegarde ignorée.");
                    return;
                }

                var pos = pedEntity.Position;
                float heading = pedEntity.Heading;

                using var scope = ServerBootstrapper.ServiceProvider.CreateScope();

                var playerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();
                var charService = scope.ServiceProvider.GetRequiredService<ICharacterService>();

                var playerDb = await playerService.GetPlayerByLicenseAsync(cleanLicense);

                if (playerDb == null)
                {
                    Debug.WriteLine("[PlayerDropped] Joueur introuvable en DB.");
                    return;
                }

                var character = await playerService.GetCharacterAsync(playerDb.ID);

                if (character == null)
                {
                    Debug.WriteLine("[PlayerDropped] Aucun personnage à sauvegarder.");
                    return;
                }

                bool success = await charService.UpdateCharacterPositionAsync(
                    character.ID,
                    pos.X,
                    pos.Y,
                    pos.Z,
                    heading
                );

                if (success)
                {
                    Debug.WriteLine($"[PlayerDropped] Position sauvegardée pour {player.Name} (raison: {reason}).");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[PlayerDropped ERROR] {ex}");
            }
        }
    }
}