using DiscordBot.Services;

namespace DiscordBot.Commands
{
    public class BanPlayerCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IPlayerInterface _playerService;

        public BanPlayerCommand(IPlayerInterface playerService)
        {
            _playerService = playerService;
        }

        [SlashCommand("ban", "Ban un joueur par son DiscordID")]
        public async Task BanAsync(string DiscordID, string Reason, double Duration)
        {
            await DeferAsync();

            var expiresAt = DateTime.UtcNow.AddHours(Duration);

            var result = await _playerService.BanPlayerToDiscordAsync(DiscordID, Reason, expiresAt);

            if (result == null)
            {
                await FollowupAsync("❌ Impossible de bannir le joueur.");
                return;
            }

            if (result.BanCountAfter == 2)
            {
                var user = Context.Guild.GetUser(ulong.Parse(DiscordID));
                if (user != null)
                    await user.KickAsync(Reason);

                await FollowupAsync("⚠️ Joueur sanctionné en DB et kick du serveur.");
            }
            else if (result.BanCountAfter >= 3)
            {
                await Context.Guild.AddBanAsync(ulong.Parse(DiscordID), reason: Reason);

                await FollowupAsync("🚫 Joueur sanctionné en DB et banni du serveur.");
            }
            else
            {
                await FollowupAsync("✅ Sanction enregistrée en DB.");
            }
        }
    }
}
