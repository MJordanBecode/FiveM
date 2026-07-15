using System;
using CitizenFX.Core;
using Lostgen.Server.Interfaces;
using Shared.DTOS;
using Lostgen.Services;

namespace Lostgen.Server.Controllers
{
    public class ConnectionController
    {
        private readonly IApiService _apiService;
        private readonly IPlayerManager _playerManager;
        private readonly EventHandlerDictionary _eventHandlers; // On va stocker les events ici

        // On injecte l'EventHandlerDictionary de FiveM directement dans le constructeur !
        public ConnectionController(IApiService apiService, IPlayerManager playerManager, EventHandlerDictionary eventHandlers)
        {
            _apiService = apiService;
            _playerManager = playerManager;
            _eventHandlers = eventHandlers;

            // On s'abonne aux événements natifs en utilisant le dictionnaire injecté
            _eventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            _eventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
        }

        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic kickReason, dynamic deferrals)
        {
            deferrals.defer();
            await BaseScript.Delay(0);

            deferrals.update($"Bonjour {playerName}, authentification en cours...");

            // Extraction des identifiants FiveM
            string license = player.Identifiers["license"];
            string discord = player.Identifiers["discord"];
            string steam = player.Identifiers["steam"];
            string ip = player.EndPoint;

            if (string.IsNullOrEmpty(license))
            {
                deferrals.done("Erreur : Impossible de récupérer votre licence Rockstar.");
                return;
            }

            // 1. On prépare la requête
            var requestData = new ConnectionRequestDto
            {
                PlayerName = playerName,
                License = license,
                DiscordId = discord,
                SteamId = steam,
                IpAddress = ip
            };

            // 2. Envoi à l'API
            var response = await _apiService.PostAsync<ConnectionRequestDto, ConnectionResponseDto>("auth/connect", requestData);

            // 3. Traitement
            if (response != null && response.IsAllowed)
            {
                _playerManager.AddPlayer(int.Parse(player.Handle), license);
                deferrals.done();
            }
            else
            {
                string reason = response?.RejectReason ?? "Impossible de contacter le serveur d'authentification.";
                deferrals.done(reason);
            }
        }

        private void OnPlayerDropped([FromSource] Player player, string reason)
        {
            int serverId = int.Parse(player.Handle);
            _playerManager.RemovePlayer(serverId);
        }
    }
}