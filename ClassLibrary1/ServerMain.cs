using System;
using CitizenFX.Core; // Pense à ajouter la référence à CitizenFX.Core.Server via NuGet

namespace Lostgen.Server
{
    // C'est l'héritage de BaseScript qui indique à FiveM de charger cette classe
    public class ServerMain : BaseScript
    {
        // Le constructeur fait office de "Point d'entrée" au démarrage de la ressource
        public ServerMain()
        {
            // On écoute l'événement de connexion ici
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic>(OnPlayerConnecting);

            Debug.WriteLine("[LostgenCore] Le Core Serveur C# est initialisé avec succès !");
        }

        private void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic deferrals)
        {
            Debug.WriteLine($"^2[Connexion]^7 {playerName} tente de se connecter.");

            // Récupération des identifiants via le dictionnaire natif de FiveM
            string steam = player.Identifiers["steam"] ?? "Non lié";
            string discord = player.Identifiers["discord"] ?? "Non lié";
            string license = player.Identifiers["license"] ?? "Non lié";
            string ip = player.Identifiers["ip"] ?? "Masquée";

            Debug.WriteLine($"--- Infos du joueur {playerName} ---");
            Debug.WriteLine($"Rockstar License: {license}");
            Debug.WriteLine($"Steam ID: {steam}");
            Debug.WriteLine($"Discord ID: {discord}");
            Debug.WriteLine($"IP Address: {ip}");
            Debug.WriteLine("-------------------------------------");
        }
    }
}