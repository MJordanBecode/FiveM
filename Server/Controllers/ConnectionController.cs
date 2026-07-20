//using System;
//using CitizenFX.Core;
//using Lostgen.Server.Managers;
//using Shared.DTOS;
//using Lostgen.Services;

//namespace Lostgen.Server.Controllers
//{
//    // En héritant de BaseScript, FiveM l'instancie automatiquement et lui donne accès aux EventHandlers
//    public class ConnectionController : BaseScript
//    {
//        private readonly IApiService _apiService;
//        private readonly IPlayerManager _playerManager;

//        // Le constructeur récupère désormais ses dépendances depuis notre Bootstrapper (voir étape suivante)
//        public ConnectionController(IApiService apiService, IPlayerManager playerManager)
//        {
//            _apiService = apiService;
//            _playerManager = playerManager;

//            // Plus besoin d'injecter le dictionnaire, on utilise la propriété héritée de BaseScript
//            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
//            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
//        }

//        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic kickReason, dynamic deferrals)
//        {
//            deferrals.defer();
//            // Utilisation du Delay hérité directement de BaseScript
//            await Delay(0);

//            deferrals.update($"Bonjour {playerName}, authentification en cours...");

//            // Extraction des identifiants FiveM
//            string license = player.Identifiers["license"];
//            string discord = player.Identifiers["discord"];
//            string steam = player.Identifiers["steam"];
//            string ip = player.EndPoint;

//            if (string.IsNullOrEmpty(license))
//            {
//                deferrals.done("Erreur : Impossible de récupérer votre licence Rockstar.");
//                return;
//            }

//            // 1. On prépare la requête
//            var requestData = new ConnectionRequestDto
//            {
//                PlayerName = playerName,
//                License = license,
//                DiscordId = discord,
//                SteamId = steam,
//                IpAddress = ip
//            };

//            // 2. Envoi à l'API
//            var response = await _apiService.PostAsync<ConnectionRequestDto, ConnectionResponseDto>("auth/connect", requestData);

//            // 3. Traitement
//            if (response != null && response.IsAllowed)
//            {
//                _playerManager.AddPlayer(int.Parse(player.Handle), license);
//                deferrals.done();
//            }
//            else
//            {
//                string reason = response?.RejectReason ?? "Impossible de contacter le serveur d'authentification.";
//                deferrals.done(reason);
//            }
//        }

//        private void OnPlayerDropped([FromSource] Player player, string reason)
//        {
//            int serverId = int.Parse(player.Handle);
//            _playerManager.RemovePlayer(serverId);
//        }
//    }
//}