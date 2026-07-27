using Discord;
using Discord.Interactions;
using DiscordBot.Interfaces;
using System;
using System.Threading.Tasks;


namespace DiscordBot.Commands
{
    public class WhiteListCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IPlayerInterface _playerService;

        public WhiteListCommand(IPlayerInterface playerService)
        {
            _playerService = playerService;
        }

        [SlashCommand("whitelist-setup", "Envoie le message interactif de liste blanche dans le salon")]
        public async Task WhitelistSetupAsync()
        {
            // Pas de DeferAsync ici si on répond directement avec le message public !
            var showWhitelistEmbed = new EmbedBuilder()
                .WithTitle("🛡️ WhiteList serveur")
                .WithDescription("Pour vous enregistrer et accéder au serveur FiveM, cliquez sur le bouton ci-dessous.")
                .WithFooter("En cliquant sur le bouton, vous acceptez le règlement du serveur.")
                .WithColor(Color.Green);

            var builder = new ComponentBuilder()
                .WithButton("Demander la WhiteList", customId: "whitelist_button", style: ButtonStyle.Success);

            // On poste le message avec le bouton dans le salon courant
            await RespondAsync(embed: showWhitelistEmbed.Build(), components: builder.Build());
        }

        [ComponentInteraction("whitelist_button")]
        public async Task HandleWhitelistButton()
        {
            await DeferAsync(ephemeral: true);

            try
            {
                Console.WriteLine($"Context.User.Id = {Context.User.Id}");
                Console.WriteLine($"Context.User.Username = {Context.User.Username}");
                Console.WriteLine($"Context.User.GlobalName = {Context.User.GlobalName}");

                ulong targetChannelId = 1516826402979315853;
                var channel = Context.Client.GetChannel(targetChannelId) as IMessageChannel;

                var userPseudo = Context.User.GlobalName ?? Context.User.Username;
                ulong userID = Context.User.Id;
                var color = new Color(184, 247, 74);

                Console.WriteLine($"[Bouton WL] Clic reçu de : {userPseudo} ({userID})");

                // 1. Vérification AVANT de lancer la whitelist
                var existingPlayer = await _playerService.GetPlayerByDiscordByIDAsync(userID);

                if (existingPlayer == null)
                {
                    await FollowupAsync(
                        "❌ Votre compte Discord n'est pas enregistré dans notre base de données. Veuillez contacter un administrateur.",
                        ephemeral: true
                    );
                    return;
                }

                if (existingPlayer.IsWhitelist)
                {
                    await FollowupAsync(
                        $"ℹ️ {Context.User.Mention}, vous êtes déjà enregistré sur la Whitelist !",
                        ephemeral: true
                    );
                    return;
                }

                // 2. Passage whitelist
                var playerResult = await _playerService.WhiteListPlayer(userID);

                if (playerResult == null)
                {
                    await FollowupAsync(
                        "❌ Une erreur est survenue pendant votre inscription à la Whitelist.",
                        ephemeral: true
                    );
                    return;
                }

                // 3. Message joueur
                await FollowupAsync(
                    $"🎉 {Context.User.Mention}, vous avez été ajouté à la Whitelist avec succès !",
                    ephemeral: true
                );

                // 4. Log staff
                if (channel != null)
                {
                    var logEmbed = new EmbedBuilder()
                        .WithTitle("Nouvelle Whitelist")
                        .WithDescription(
                            $"**Utilisateur :** {Context.User.Mention} ({userPseudo})\n" +
                            $"**Discord ID :** `{userID}`\n" +
                            $"**Statut :** Activé sur Discord et FiveM"
                        )
                        .WithFooter($"Le {DateTime.Now:dd/MM/yyyy à HH:mm}")
                        .WithColor(color);

                    await channel.SendMessageAsync(embed: logEmbed.Build());
                }
                else
                {
                    Console.WriteLine($"[Erreur Log] Impossible de trouver le salon {targetChannelId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erreur Whitelist] Exception : {ex.Message}");
                await FollowupAsync("⚠️ Une erreur serveur interne est survenue lors du traitement de votre requête.", ephemeral: true);
            }
        }
    }
}