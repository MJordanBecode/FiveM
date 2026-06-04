using Discord;
using Discord.Interactions;
using DiscordBot.Interfaces;

namespace DiscordBot.Commands
{
    public class PrintCompaniesCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("showcompanies","print all companies available")]
        public async Task PrintCompaniesAsync()
        {
            var ShowCompaniesEmbed = new EmbedBuilder()
                .WithTitle("Liste des entreprises disponibles")
                .WithDescription("Voici la liste des entreprises disponibles :\n" + string.Join("\n", Enum.GetValues(typeof(CompaniesEnum)).Cast<CompaniesEnum>().Select(c => $"- {c}")))
                .WithColor(Color.Blue);
                

            await RespondAsync(embed: ShowCompaniesEmbed.Build());
        }
    }
}
