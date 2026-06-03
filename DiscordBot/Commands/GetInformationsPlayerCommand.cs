using Discord;
using Discord.Interactions;
using DiscordBot.Interfaces;
using DiscordBot.ModelMongoose;

namespace DiscordBot.Commands
{
    public class GetInformationsPlayerCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IPlayerInterface _playerService;

        public GetInformationsPlayerCommand(IPlayerInterface playerService)
        {
            _playerService = playerService;
        }

        [SlashCommand("profil", "Affiche le profil complet d'un joueur")]
        public async Task ProfilAsync(string playerInfo)
        {
            var player = await _playerService.GetPlayerByDiscordByIDOrPseudoAsync(playerInfo);

            if (player == null)
            {
                await RespondAsync($"❌ Aucun profil trouvé pour `{playerInfo}`.", ephemeral: true);
                return;
            }

            var createdAt = new DateTimeOffset(player.CreatedAt).ToUnixTimeSeconds();
            var updatedAt = new DateTimeOffset(player.UpdatedAt).ToUnixTimeSeconds();

            var color = player.Grade.ToLower() switch
            {
                "admin" => Color.Red,
                "modérateur" => Color.Orange,
                "moderator" => Color.Orange,
                "vip" => Color.Gold,
                "member" => Color.Blue,
                _ => Color.DarkGrey
            };

            var embed = new EmbedBuilder()
                .WithAuthor($"Profil joueur • {player.DiscordPseudo}", player.AvatarUrl)
                .WithTitle("🪪 Carte d'identité RP")
                .WithDescription(
                    $"Bienvenue sur la fiche officielle de **{player.DiscordPseudo}**.\n" +
                    $"Voici les informations enregistrées dans la base de données.")
                .WithColor(color)
                .WithThumbnailUrl(player.AvatarUrl)
                .AddField("👤 Identité Discord",
                    $"**Nom :** `{player.DiscordName}`\n" +
                    $"**Pseudo RP :** `{player.DiscordPseudo}`\n" +
                    $"**Discord ID :** `{player.DiscordID}`", false)
                .AddField("📊 Progression",
                    $"**Niveau :** `{player.Level}`\n" +
                    $"**XP :** `{player.Xp}`\n" +
                    $"**Grade :** `{player.Grade}`", true)
                .AddField("🛡️ Statut",
                    player.IsDeleted ? "🔴 Profil supprimé" : "🟢 Profil actif", true)
                .AddField("📅 Historique",
                    $"**Créé :** <t:{createdAt}:F>\n" +
                    $"**Mis à jour :** <t:{updatedAt}:R>", false)
                .WithImageUrl(player.AvatarUrl)
                .WithFooter($"Demandé par {Context.User.Username}", Context.User.GetAvatarUrl())
                .WithCurrentTimestamp();

            await RespondAsync(embed: embed.Build());
        }
    }
}