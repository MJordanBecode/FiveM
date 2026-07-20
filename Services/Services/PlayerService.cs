using Shared.VModels;
using Services.Interfaces;
using Data.Repositories;
using Data.Models;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Services.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerService(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayersVM> CreatePlayerAsync(string license, string steamHex, ulong discordId)
        {

            Debug.WriteLine($"Discord reçu : {discordId}");

            // 1. On récupère le joueur grâce à son compte Discord pré-créé
            Players dbPlayer = await _playerRepository.GetPlayerByDiscordIdAsync(discordId);

            Debug.WriteLine(dbPlayer == null
                ? "JOUEUR INTROUVABLE"
                : "JOUEUR TROUVE");

            if (dbPlayer == null)
            {
                // Le joueur n'a pas été trouvé (il n'a pas dû faire sa WL sur Discord)
                return new PlayersVM { IsWhitelisted = false };
            }

            Debug.WriteLine("Joueur trouvé !");
            Debug.WriteLine($"ConnectionOnce = {dbPlayer.ConnectionOnce}");

            // 2. Si ConnectionOnce est à false (0 dans ta BDD), c'est son premier saut en jeu !
            if (!dbPlayer.ConnectionOnce)
            {
                // Sécurité si la relation n'était pas instanciée en C#
                if (dbPlayer.Identifier == null)
                {
                    dbPlayer.Identifier = new Identifiers();
                }

                dbPlayer.Identifier.FiveMLicense = license;
                dbPlayer.Identifier.SteamLicense = steamHex;

                Debug.WriteLine($"FiveM : {dbPlayer.Identifier.FiveMLicense}");
                Debug.WriteLine($"Steam : {dbPlayer.Identifier.SteamLicense}");

                dbPlayer.ConnectionOnce = true;




                // On sauvegarde tout d'un coup en BDD
                Debug.WriteLine("Avant SaveChanges");
                await _playerRepository.SaveChangesAsync();
                Debug.WriteLine("Après SaveChanges");
            }
            else
            {
                Debug.WriteLine("Le joueur s'est déjà connecté auparavant.");
            }

            // 3. On prépare le ViewModel pour le serveur de jeu
            PlayersVM playerVm = new PlayersVM
            {
                ConnectionOnce = dbPlayer.ConnectionOnce,
                IsWhitelisted = dbPlayer.IsWhitelisted
                // Ajoute ici tes autres mappings (FirstName, LastName...) quand ils ne seront plus NULL
            };

            return playerVm;
        }
    }
}