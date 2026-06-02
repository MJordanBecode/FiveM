using DiscordBot.ModelMongoose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class PlayerService : IPlayerInterface
    {
        public Task<PlayerInformations?> BanPlayerToDiscord(ulong DiscordID, string Reason, TimeSpan Duration)
        {
            throw new NotImplementedException();
        }

        public Task CreatePlayerByID(PlayerInformations player)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerInformations>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerInformations?> GetPlayerByDiscordIDAsync(PlayerInformations player)
        {
            //ulong DiscordID = player.DiscordID;

            throw new NotImplementedException();
        }

        public Task<PlayerInformations?> KickPlayerToDiscord(ulong DiscordID, string Reason, TimeSpan Duration)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeletePlayerID(string DiscordID)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerInformations?> UpdateLevelPlayerByIDAsync(ulong DiscordID, ushort NewLevel)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerInformations?> UpdateXpPlayerByIDAsync(ulong DiscordID, int XpToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
