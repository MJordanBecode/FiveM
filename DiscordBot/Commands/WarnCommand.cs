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

            ulong targetChannelId = 1510288766420521031;
            var channel = Context.Client.GetChannel(targetChannelId) as IMessageChannel;

            if (channel == null)
            {
                await FollowupAsync("❌ Le salon de logs est introuvable.", ephemeral: true);
                return;
            }

            ulong discordId = user.Id;

            var ModeratorName = Context.User.Username;

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

                await channel.SendMessageAsync(
                    $"🔨 **BAN AUTOMATIQUE**\n" +
                    $"**Joueur :** {player.DiscordName}\n" +
                    $"**Discord :** {user.Mention} (`{discordId}`)\n" +
                    $"**Raison :** 3 warns atteints\n" +
                    $"**Dernier warn :** {reason}\n" +
                    $"**Modérateur :** {Context.User.Mention}"
                );

                return;
            }

            await FollowupAsync(
                $"⚠️ **{player.DiscordName}** a reçu un avertissement.\n" +
                $"**Warns :** `{newWarnCount}/3`\n" +
                $"**Raison :** {reason}",
                ephemeral: true
            );

            var ShowWarnEmbed = new EmbedBuilder()
    .WithTitle("⚠️ Avertissement enregistré")
    .WithColor(new Color(255, 170, 0))
    .WithThumbnailUrl(user.GetDisplayAvatarUrl())
    .WithDescription("Un avertissement a été ajouté au dossier du joueur.")
    .AddField(
        "👤 Informations joueur",
        $"**Nom** : {player.DiscordName}\n" +
        $"**Mention** : {user.Mention}\n" +
        $"**ID** : `{discordId}`",
        true
    )
    .AddField(
        "📋 Sanction",
        $"**Type** : Warn\n" +
        $"**Compteur** : `{newWarnCount}/3`\n" +
        $"**Statut** : Actif",
        true
    )
    .AddField(
        "📝 Motif",
        $"```{reason}```",
        false
    )
    .AddField(
        "🛡️ Modération",
        $"**Staff** : {Context.User.Mention}\n" +
        $"**Date** : <t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
        false
    )
    .WithFooter("Système de modération")
    .WithCurrentTimestamp()
    .Build();

            await channel.SendMessageAsync(embed: ShowWarnEmbed);
        }
    }
}