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
                        Debug.WriteLine(
                            "[PlayerLoaded] Le personnage existe mais aucun skin trouvé."
                        );

                        return;
                    }



                    string skinJson =
                        Newtonsoft.Json.JsonConvert.SerializeObject(
                            character.Skin
                        );

                    Debug.WriteLine(
                        $"Skin Face : {character.Skin?.Face}");

                    Debug.WriteLine(
                        $"Skin Hair : {character.Skin?.Hair}");

                    Debug.WriteLine(
                        $"Skin Clothes : {character.Skin?.Clothes}");

                    TriggerClientEvent(
                        player,
                        "lostgen:client:loadCharacter",
                        skinJson
                    );
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