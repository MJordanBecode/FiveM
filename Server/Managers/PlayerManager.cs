using System.Collections.Generic;
using CitizenFX.Core;
using Lostgen.Server.Interfaces;

namespace Lostgen.Server.Services
{
    public class PlayerManager : IPlayerManager
    {
        // Dictionnaire pour stocker les joueurs (ID Serveur -> Clé de Licence)
        // Plus tard, tu remplaceras "string" par ta propre classe "FrameworkPlayer"
        private readonly Dictionary<int, string> _connectedPlayers = new Dictionary<int, string>();

        public void AddPlayer(int serverId, string license)
        {
            if (!_connectedPlayers.ContainsKey(serverId))
            {
                _connectedPlayers.Add(serverId, license);
                Debug.WriteLine($"[PlayerManager] Joueur {serverId} (License: {license}) enregistré en mémoire.");
            }
        }

        public void RemovePlayer(int serverId)
        {
            if (_connectedPlayers.ContainsKey(serverId))
            {
                _connectedPlayers.Remove(serverId);
                Debug.WriteLine($"[PlayerManager] Joueur {serverId} retiré de la mémoire.");
            }
        }

        public bool IsPlayerConnected(int serverId)
        {
            return _connectedPlayers.ContainsKey(serverId);
        }
    }
}