using DiscordBot.ModelMongoose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Interfaces
{
    public interface IPlayerInterface
    {
        Task CreatePlayerByID(PlayerInformations player);
        Task SoftDeletePlayerID(string DiscordID);
        Task <List<PlayerInformations>> GetAllPlayersAsync(); // Get a list of all players
        Task<PlayerInformations?> GetPlayerByDiscordIDAsync(PlayerInformations player); // Get player information by their Discord ID
        Task<PlayerInformations?> UpdateXpPlayerByIDAsync(ulong DiscordID, int XpToAdd); // Update the player's XP by their Discord ID
        Task<PlayerInformations?> UpdateLevelPlayerByIDAsync(ulong DiscordID, ushort NewLevel); // Update the player's level by their Discord ID
        Task<PlayerInformations?> KickPlayerToDiscord(ulong DiscordID, string Reason, TimeSpan Duration);
        Task<PlayerInformations?> BanPlayerToDiscord(ulong DiscordID, string Reason, TimeSpan Duration);

    }
}
