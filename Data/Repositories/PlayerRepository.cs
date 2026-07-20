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

        public async Task<Players> CreatePlayerIdentifierAsync(string license, string steamHex, ulong discordId)
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
                        DiscordLicense = discordId,
                    },

                    ConnectionOnce = false,
                    IsWhitelisted = discordId != 0,
                };

                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                if (player.Identifier.DiscordLicense != discordId)
                {
                    player.Identifier.DiscordLicense = discordId;
                }
            }

            return player;
        }

        public async Task<Players> GetPlayerByLicenseAsync(string license)
        {
            return await _dbContext.Players
                            .Include(p => p.Identifier)
                            .FirstOrDefaultAsync(p => p.Identifier.FiveMLicense == license);
        }

        // Recherche le joueur via son ID Discord (créé au préalable par le bot)
        public async Task<Players> GetPlayerByDiscordIdAsync(ulong discordId)
        {
            return await _dbContext.Players
                            // On inclut les identifiants liés pour pouvoir les modifier après
                            .Include(p => p.Identifier)
                            // On cherche le joueur dont l'un des identifiants possède le bon Discord ID
                            .FirstOrDefaultAsync(p => p.Identifier.DiscordLicense == discordId);
        }

        public async Task SaveChangesAsync()
        {
            Debug.WriteLine("===== CHANGE TRACKER =====");

            foreach (var e in _dbContext.ChangeTracker.Entries())
            {
                Debug.WriteLine($"{e.Entity.GetType().Name} -> {e.State}");
            }

            Debug.WriteLine("==========================");

            await _dbContext.SaveChangesAsync();
        }
    }
}