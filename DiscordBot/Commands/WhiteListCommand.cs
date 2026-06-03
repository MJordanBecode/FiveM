using Discord;
using Discord.Interactions;
using DiscordBot.Interfaces;

namespace DiscordBot.Commands
{
    public class WhiteListCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("whitelist", "Ajoute un joueur à la liste blanche")]
        public async Task WhitelistAsync()
        {
            var Test = new EmbedBuilder()
                .WithTitle("WhiteList Discord")
                .WithDescription("Pour vous white lister, cliquez sur le bouton ci-dessous.")
                .WithFooter("En cliquant sur le bouton, vous acceptez les règles du serveur.")
                .WithColor(Color.Green);

            var builder = new ComponentBuilder().WithButton("WhiteList", customId: "whitelist_button", style: ButtonStyle.Success);
            await RespondAsync(embed: Test.Build(), components: builder.Build());
            
        }
        [ComponentInteraction("whitelist_button")]
        public async Task HandleWhitelistButton()
        {            
            await RespondAsync($"{Context.User.Mention} a été ajouté à la liste blanche !", ephemeral: true);
        }
    }

}
