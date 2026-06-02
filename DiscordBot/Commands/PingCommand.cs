
namespace DiscordBot.Commands
{
    public class PingCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("pingpong", "Responds with a ping.")]
        public async Task PingAsync()
        {
            await RespondAsync("Pong!");
        }
    }
}
