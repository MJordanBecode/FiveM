using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PlayerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Players> CreatePlayerIdentifierAsync(
            string license,
            string steamHex,
            ulong discordId)
        {
            var player = await GetPlayerByLicenseAsync(license);


            if (player == null)
            {
                player = new Players
                {
                    Identifier = new Identifiers
                    {
                        FiveMLicense = license,
                        SteamLicense = steamHex,
                        DiscordLicense = discordId
                    },

                    ConnectionOnce = false,
                    IsWhitelisted = discordId != 0
                };


                _dbContext.Players.Add(player);

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Mise à jour Discord si nécessaire
                if (player.Identifier != null &&
                    player.Identifier.DiscordLicense != discordId)
                {
                    player.Identifier.DiscordLicense = discordId;

                    await _dbContext.SaveChangesAsync();
                }
            }


            return player;
        }



        public async Task<Players?> GetPlayerByLicenseAsync(string license)
        {
            return await _dbContext.Players

                // Compte FiveM
                .Include(p => p.Identifier)

                // Personnage(s)
                .Include(p => p.Characters)
                    .ThenInclude(c => c.Skin)

                .FirstOrDefaultAsync(
                    p => p.Identifier!.FiveMLicense == license
                );
        }



        // Recherche via Discord (bot Discord)
        public async Task<Players?> GetPlayerByDiscordIdAsync(
            ulong discordId)
        {
            return await _dbContext.Players

                .Include(p => p.Identifier)

                .Include(p => p.Characters)
                    .ThenInclude(c => c.Skin)

                .FirstOrDefaultAsync(
                    p => p.Identifier!.DiscordLicense == discordId
                );
        }



        public async Task SaveChangesAsync()
        {
            Debug.WriteLine("===== CHANGE TRACKER =====");


            foreach (var e in _dbContext.ChangeTracker.Entries())
            {
                Debug.WriteLine(
                    $"{e.Entity.GetType().Name} -> {e.State}"
                );
            }


            Debug.WriteLine("==========================");


            await _dbContext.SaveChangesAsync();
        }

        public async Task<PlayerCharacters> GetCharacterAsync(Guid playerId)
        {
            return await _dbContext.PlayerCharacters

                .Include(c => c.Skin)

                .FirstOrDefaultAsync(
                    c => c.PlayerID == playerId
                );
        }


        public async Task<bool> HasCharacterAsync(Guid playerId)
        {
            return await _dbContext.PlayerCharacters
                .AnyAsync(
                    c => c.PlayerID == playerId
                );
        }
    }
}