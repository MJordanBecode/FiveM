using Discord;
using Discord.Interactions;
using DiscordBot.Enums;
using DiscordBot.Interfaces;

namespace DiscordBot.Commands
{
    public class WarnCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IPlayerInterface _playerService;

        public WarnCommand(IPlayerInterface playerService)
        {
            _playerService = playerService;
        }

        [SlashCommand("warn", "Warn a user")]
        public async Task PunishAsync(IGuildUser user, string reason)
        {
            await DeferAsync(ephemeral: true);

            ulong discordId = user.Id;

            var player = await _playerService.GetPlayerByDiscordByIDAsync(discordId);

            if (player == null)
            {
                await FollowupAsync(
                    $"❌ Aucun joueur trouvé en base pour **{user.Username}**.",
                    ephemeral: true
                );
                return;
            }

            var currentWarnCount = await _playerService.PunishmentPlayerCount(
                discordId,
                PunishmentTypes.Warn
            );

            var newWarnCount = currentWarnCount + 1;

            await _playerService.WarnPlayerByDiscordIDAsync(
                discordId,
                reason,
                PunishmentTypes.Warn
            );

            if (newWarnCount >= 3)
            {
                await _playerService.BanPlayerToDiscordAsync(
                    discordId,
                    "3 warns atteints",
                    DateTime.UtcNow.AddYears(100)
                );

                await Context.Guild.AddBanAsync(
                    discordId,
                    pruneDays: 0,
                    reason: "3 warns atteints"
                );

                await FollowupAsync(
                    $"🔨 **{player.DiscordName}** a reçu son 3ème warn et a été banni.\n" +
                    $"**Raison du dernier warn :** {reason}",
                    ephemeral: true
                );

                return;
            }

            await FollowupAsync(
                $"⚠️ **{player.DiscordName}** a reçu un avertissement.\n" +
                $"**Warns :** `{newWarnCount}/3`\n" +
                $"**Raison :** {reason}",
                ephemeral: true
            );
        }
    }
}