// Dans le projet Lostgen.Data (qui possède EF Core)
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Disponible ici !
using Data.Context;
using Data.Models;

namespace Data.Repositories
{
    public class PlayerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // C'est cette méthode qui va exécuter le code EF Core 
        public async Task<Players> CreatePlayerIdentifierAsync(string license, string steamHex)
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
                    },

                    ConnectionOnce = false,
                    IsWhitelisted = true,

                };

                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
            }

            return player;
        }

        public async Task<Players> GetPlayerByLicenseAsync(string license)
        {
            return await _dbContext.Players
                            .Include(p => p.Identifier)
                            .FirstOrDefaultAsync(p => p.Identifier.FiveMLicense == license);
        }
    }
}