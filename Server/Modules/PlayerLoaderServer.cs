using CitizenFX.Core;
using CitizenFX.Core.Native;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using System;
using System.Globalization;
using static Shared.DTOS.SkinDataDto;

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



                using var scope = ServerBootstrapper.ServiceProvider.CreateScope();


                var playerService =
                    scope.ServiceProvider.GetRequiredService<IPlayerService>();



                var playerDb =
                    await playerService.GetPlayerByLicenseAsync(cleanLicense);



                if (playerDb == null)
                {
                    Debug.WriteLine("[PlayerLoaded] Joueur introuvable.");
                    return;
                }



                await Delay(0);



                bool hasCharacter =
                    await playerService.HasCharacterAsync(playerDb.ID);



                if (!hasCharacter)
                {
                    Debug.WriteLine(
                        "[PlayerLoaded] Aucun personnage trouvé. Création."
                    );


                    if (int.TryParse(player.Handle, out int playerServerId))
                    {
                        int privateBucket = 1000 + playerServerId;

                        API.SetPlayerRoutingBucket(
                            player.Handle,
                            privateBucket
                        );
                    }



                    TriggerClientEvent(
                        player,
                        "lostgen:client:startCharacterCreation",
                        playerDb.ID.ToString()
                    );
                }
                else
                {
                    Debug.WriteLine(
                        "[PlayerLoaded] Personnage trouvé. Chargement."
                    );


                    var character =
                        await playerService.GetCharacterAsync(playerDb.ID);



                    if (character == null)
                    {
                        Debug.WriteLine(
                            "[PlayerLoaded] Impossible de récupérer le personnage."
                        );

                        return;
                    }



                    if (character.Skin == null)
                    {
                        Debug.WriteLine("[PlayerLoaded] Le personnage existe mais aucun skin trouvé.");
                        return;
                    }

                    var face = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<FaceDataDto>(character.Skin.Face)
                        ?? new FaceDataDto();

                    var hair = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<HairDataDto>(character.Skin.Hair)
                        ?? new HairDataDto();

                    var clothes = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<ClothesDataDto>(character.Skin.Clothes)
                        ?? new ClothesDataDto();

                    // Extraction des composants (avec valeurs par défaut si absents)
                    clothes.Components.TryGetValue(11, out var torso);
                    clothes.Components.TryGetValue(4, out var pants);
                    clothes.Components.TryGetValue(6, out var shoes);

                    // 🟢 Position sauvegardée, avec fallback si jamais définie (premier chargement après migration par ex.)
                    float posX = character.PositionX ?? -1037.7f;
                    float posY = character.PositionY ?? -2737.8f;
                    float posZ = character.PositionZ ?? 20.1f;
                    float heading = character.Heading ?? 0f;

                    string payload = string.Join("|",
                        character.Gender == 'F' ? "1" : "0",
                        face.FatherShape, face.MotherShape, face.ShapeMix.ToString(CultureInfo.InvariantCulture),
                        face.FatherSkin, face.MotherSkin, face.SkinMix.ToString(CultureInfo.InvariantCulture),
                        hair.Style, hair.Texture, hair.Color, hair.HighlightColor,
                        torso?.Drawable ?? 0, torso?.Texture ?? 0,
                        pants?.Drawable ?? 0, pants?.Texture ?? 0,
                        shoes?.Drawable ?? 0, shoes?.Texture ?? 0,
                        posX.ToString(CultureInfo.InvariantCulture),
                        posY.ToString(CultureInfo.InvariantCulture),
                        posZ.ToString(CultureInfo.InvariantCulture),
                        heading.ToString(CultureInfo.InvariantCulture)
                    );

                    Debug.WriteLine("[PlayerLoaded] Envoi du skin au client...");

                    TriggerClientEvent(player, "lostgen:client:loadCharacter", payload);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[PlayerLoaded ERROR] {ex}"
                );
            }
        }
    }
}