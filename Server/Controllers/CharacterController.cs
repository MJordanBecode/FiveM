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
            EventHandlers["lostgen:server:saveCharacter"] += new Action<Player, string, string, string, string, string, short, string, string, string>(OnSaveCharacter);
        }

        private async void OnSaveCharacter(
            [FromSource] Player player,
            string playerIdStr,
            string firstName,
            string lastName,
            string birthDayStr,
            string genderStr,
            short height,
            string faceJson,
            string hairJson,
            string clothesJson)
        {
            try
            {
                if (!Guid.TryParse(playerIdStr, out Guid playerId)) return;

                DateTime birthDay = DateTime.Parse(birthDayStr);
                char gender = genderStr[0];

                var face = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<FaceDataDto>(faceJson)
                    ?? new FaceDataDto();


                var hair = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<HairDataDto>(hairJson)
                    ?? new HairDataDto();


                var clothes = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<ClothesDataDto>(clothesJson)
                    ?? new ClothesDataDto();

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
                    face,
                    hair,
                    clothes
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
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}