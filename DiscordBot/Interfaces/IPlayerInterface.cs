using DiscordBot.ModelMongoose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiscordBot.Services.PlayerService;

namespace DiscordBot.Interfaces
{
    public interface IPlayerInterface
    {
        Task CreatePlayerByID(PlayerInformations player);
        Task SoftDeletePlayerID(string DiscordID);
        Task HardDeletePlayerID(string DiscordID);
        Task <List<PlayerInformations>> GetAllPlayersAsync(); // Get a list of all players
        Task <PlayerInformations?> GetPlayerByDiscordIDAsync(string DiscordID); // Get player information by their Discord ID
        Task<PlayerInformations?> GetPlayerByDiscordByIDOrPseudoAsync(string PlayerInfo); // Get player information by their Discord ID or optionally by their Discord Pseudo
        Task<PlayerInformations?> UpdateXpPlayerByIDAsync(string DiscordID, int XpToAdd); // Update the player's XP by their Discord ID
        Task<PlayerInformations?> UpdateLevelPlayerByIDAsync(string DiscordID, ushort NewLevel); // Update the player's level by their Discord ID
        Task<PlayerInformations?> KickPlayerToDiscord(string DiscordID, string Reason, DateTime? ExpiresAt);
        Task<BanPlayerResult?> BanPlayerToDiscordAsync(string DiscordID, string Reason, DateTime? ExpiresAt);

    }
}
