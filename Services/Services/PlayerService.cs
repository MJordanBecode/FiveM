using Shared.VModels;
using Services.Interfaces;
using Data.Repositories;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerRepository _playerRepository;

        // Le constructeur demande le repository (fourni par ton Bootstrapper)
        public PlayerService(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayersVM> CreatePlayerAsync(string license, string steamHex, string discordId, string playerName)
        {
            // 1. On appelle ta méthode de Data pour récupérer ou créer le joueur
            Players dbPlayer = await _playerRepository.CreatePlayerIdentifierAsync(license, steamHex);

            // Pour l'instant on ignore l'id discord et le nom dans la BDD, mais on pourra les save plus tard !

            // 2. On transforme le modèle de BDD en ViewModel (Shared)
            PlayersVM playerVm = new PlayersVM
            {
                // Mappe ici les propriétés de ton PlayersVM par rapport à dbPlayer
                // Exemple :
                ConnectionOnce = dbPlayer.ConnectionOnce,
                IsWhitelisted = dbPlayer.IsWhitelisted
            };

            // 3. On retourne le VM au script Server
            return playerVm;
        }
    }
}
