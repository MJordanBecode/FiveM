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

        [SlashCommand("ban", "Ban un joueur par son DiscordName")]
        public async Task BanAsync(ulong DiscordID, string Reason, double Duration)
        {
            await DeferAsync();

            var expiresAt = DateTime.UtcNow.AddHours(Duration);

            var CheckIfPlayerExist = await _playerService.GetPlayerByDiscordByIDAsync(DiscordID);

            if (CheckIfPlayerExist == null)
                throw new Exception("L'utilisateur pas trouvé");

            await _playerService.BanPlayerToDiscordAsync(DiscordID, Reason, expiresAt);

            await Context.Guild.AddBanAsync(ulong.Parse(CheckIfPlayerExist.DiscordID), 0, Reason);   


            await FollowupAsync($"Le joueur {CheckIfPlayerExist.DiscordName} a été banni pour la raison : {Reason} pendant {Duration} heures.");



        }
    }
}
