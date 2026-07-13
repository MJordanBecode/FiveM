using Discord;
using Discord.Interactions;
using DiscordBot.Interfaces;

namespace DiscordBot.Commands
{
    public class WhiteListCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IPlayerInterface _playerService;
        public WhiteListCommand(IPlayerInterface playerService)
        {
            _playerService = playerService;
        }

        [SlashCommand("whitelist", "Ajoute un joueur à la liste blanche")]
        public async Task WhitelistAsync()
        {
            await DeferAsync(ephemeral: true);
            

            var ShowWhitelistEmbed = new EmbedBuilder()
                .WithTitle("WhiteList Discord")
                .WithDescription("Pour vous white lister, cliquez sur le bouton ci-dessous.")
                .WithFooter("En cliquant sur le bouton, vous acceptez les règles du serveur.")
                .WithColor(Color.Green);            

            var builder = new ComponentBuilder().WithButton("WhiteList", customId: "whitelist_button", style: ButtonStyle.Success);
            await RespondAsync(embed: ShowWhitelistEmbed.Build(), components: builder.Build());
            
            
        }
        [ComponentInteraction("whitelist_button")]
        public async Task HandleWhitelistButton()
        {
            ulong targetChannelId = 1516826402979315853;
            var channel = Context.Client.GetChannel(targetChannelId) as IMessageChannel;
            var user = Context.User.GlobalName;
            var userID = 'a';
            var color = new Color(184, 247, 74); 

            if (user == null)
            {
                await FollowupAsync("❌ Impossible de récupérer le nom d'utilisateur.", ephemeral: true);
                return;
            }

            if (userID == 0)
            {
                await FollowupAsync("❌ Impossible de récupérer l'ID de l'utilisateur.", ephemeral: true);
                return;
            }

            //Faire une vérification pour voir si le joueur à déjà été whitelisté sur le serveur FiveM et Discord.


            var checkIfPlayerIsWhitelisted = await _playerService.WhiteListPlayer(userID);

            if(checkIfPlayerIsWhitelisted != null)
            {
                await FollowupAsync("❌ Vous êtes déjà sur la liste blanche.", ephemeral: true);
                return;
            }
           
            var Test = new EmbedBuilder()
                .WithTitle("WhiteList")
                .WithDescription($"{user} à été Whitelisté sur Discord et FiveM")
                .WithFooter(text: $"{DateTime.Now}")
                .WithColor(color);

            Console.Write($"Voici toutes les données récupéres au moment du clic : User: {user}, UserID: {userID}");

            if (channel == null)
            {
                await FollowupAsync("❌ GetChannel retourne null.", ephemeral: true);
                return;
            }
            await RespondAsync($"{Context.User.Mention} a été ajouté à la Whitelist !", ephemeral: true);
            await channel.SendMessageAsync(embed: Test.Build());
        }
    }

}
