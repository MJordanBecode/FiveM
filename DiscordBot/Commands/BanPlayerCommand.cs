using DiscordBot.Services;

namespace DiscordBot.Commands
{
    public class BanPlayerCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ban","Ban un joueur par son DiscordID ou son Pseudo de compte")]
        public async Task BanAsync(string DiscordID, string Reason, TimeSpan Duration, string? PseudoDiscord = null)
        {
            //var BannedPlayer = await PlayerService.Instance.BanPlayerToDiscord(DiscordID, Reason, Duration, PseudoDiscord);

        }
    }
}
