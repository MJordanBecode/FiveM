using DiscordBot.Enums;
using DiscordBot.ModelMongoose;

namespace DiscordBot.Interfaces
{
    public interface IPlayerInterface
    {
        Task CreatePlayerByID(Players player);

        Task SoftDeletePlayerID(ulong discordId);
        Task HardDeletePlayerID(ulong discordId);

        Task<List<Players>> GetAllPlayersAsync();

        Task<Players?> GetPlayerByDiscordByIDAsync(ulong discordId);

        Task<Players?> UpdateXpPlayerByIDAsync(ulong discordId, int xpToAdd);
        Task<Players?> UpdateLevelPlayerByIDAsync(ulong discordId, ushort newLevel);

        Task<Players?> KickPlayerToDiscord(ulong discordId, string reason, DateTime? expiresAt);
        Task<Players?> BanPlayerToDiscordAsync(ulong discordId, string reason, DateTime? expiresAt);
        Task<Players?> MutePlayerToDiscord(ulong discordId, string reason, DateTime? expiresAt);

        Task<Players?> WhiteListPlayer(ulong DiscordId);

        Task<bool> CheckIfPlayerIsBanned(ulong discordId);
        Task<bool> CheckIfPlayerIsWhitelisted(ulong discordId);

        Task<Punishments?> WarnPlayerByDiscordIDAsync(ulong discordId, string reason,PunishmentTypes punishmentTypes);

        Task<int> PunishmentPlayerCount(ulong discordId, PunishmentTypes punishmentType);
    }
}