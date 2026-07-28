using CitizenFX.Core;
using CitizenFX.Core.Native;
using Lostgen.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using static Shared.DTOS.SkinDataDto;

namespace Lostgen.Server.Controllers
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

                // 1) Désérialisation des DTOs "bruts" envoyés par le NUI
                var clientFace = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<ClientFaceDto>(faceJson)
                    ?? new ClientFaceDto();

                var clientHair = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<ClientHairDto>(hairJson)
                    ?? new ClientHairDto();

                var clientClothes = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<ClientClothesDto>(clothesJson)
                    ?? new ClientClothesDto();

                // 2) Mapping vers les DTOs de sauvegarde (SkinDataDto)
                var face = new FaceDataDto
                {
                    FatherShape = clientFace.shapeFirstID,
                    MotherShape = clientFace.shapeSecondID,
                    ShapeMix = clientFace.shapeMix,

                    FatherSkin = clientFace.shapeFirstID,
                    MotherSkin = clientFace.shapeSecondID,
                    SkinMix = clientFace.skinMix,

                    EyeColor = 0
                };

                var hair = new HairDataDto
                {
                    Style = clientHair.drawable,
                    Texture = clientHair.texture,
                    Color = clientHair.color,
                    HighlightColor = clientHair.highlight
                };

                var clothes = new ClothesDataDto
                {
                    Components = new Dictionary<int, ComponentDataDto>
                    {
                        [11] = new ComponentDataDto { Drawable = clientClothes.torso, Texture = clientClothes.torsoTexture }, // Veste/Haut
                        [4] = new ComponentDataDto { Drawable = clientClothes.pants, Texture = clientClothes.pantsTexture }, // Pantalon
                        [6] = new ComponentDataDto { Drawable = clientClothes.shoes, Texture = clientClothes.shoesTexture }  // Chaussures
                    }
                };

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

                await Delay(0);

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