using CitizenFX.Core;
using CitizenFX.Core.Native;
using Lostgen.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces; // Accès aux interfaces du projet Services (ICharacterService)
using Shared.DTOS;
using System;
using static Shared.DTOS.SkinDataDto;

namespace Lostgen.Server.Controllers // 🟢 Rattaché au projet Server !
{
    public class CharacterController : BaseScript
    {
        public CharacterController()
        {
            EventHandlers["lostgen:server:saveCharacter"] += new Action<Player, string, string, string, string, string, short>(OnSaveCharacter);
        }

        private async void OnSaveCharacter([FromSource] Player player, string playerIdStr, string firstName, string lastName, string birthDayStr, string genderStr, short height)
        {
            try
            {
                if (!Guid.TryParse(playerIdStr, out Guid playerId)) return;

                DateTime birthDay = DateTime.Parse(birthDayStr);
                char gender = genderStr[0];

                var defaultFace = new FaceDataDto();
                var defaultHair = new HairDataDto();
                var defaultClothes = new ClothesDataDto();

                // 🟢 Access à ServerBootstrapper sans erreur car on est DANS le projet Server !
                if (ServerBootstrapper.ServiceProvider == null) return;

                using var scope = ServerBootstrapper.ServiceProvider.CreateScope();
                var charService = scope.ServiceProvider.GetRequiredService<ICharacterService>();

                bool success = await charService.SaveCharacterCreationAsync(
                    playerId,
                    firstName,
                    lastName,
                    birthDay,
                    gender,
                    height,
                    defaultFace,
                    defaultHair,
                    defaultClothes
                );

                await Delay(0); // Retour sur le thread FiveM

                if (success)
                {
                    Debug.WriteLine($"[CharacterCreation] Personnage {firstName} {lastName} créé avec succès pour {player.Name}.");

                    API.SetPlayerRoutingBucket(player.Handle, 0);

                    Vector3 startSpawn = new Vector3(-1037.7f, -2737.8f, 20.1f);
                    player.TriggerEvent("lostgen:client:spawnPlayerAfterCreation", startSpawn.X, startSpawn.Y, startSpawn.Z);
                }
                else
                {
                    Debug.WriteLine($"[ERROR] Joueur introuvable en BDD pour l'ID : {playerId}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Échec lors de la création du personnage : {ex.Message}");
            }
        }
    }
}