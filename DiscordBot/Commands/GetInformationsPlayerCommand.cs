using DiscordBot.Database;
using DiscordBot.ModelMongoose;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class GetInformationsPlayerCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("profil", "Recherche un joueur")]
        public async Task ProfilAsync(string DiscordId)
        {
            // Plus besoin de TryParse ! On cherche directement la chaîne de caractères.
            var filter = Builders<PlayerInformations>.Filter.Eq(x => x.DiscordID, DiscordId);

            var player = await MongoCollections.Players
                .Find(filter)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                await RespondAsync($"Aucun joueur trouvé pour l'ID `{DiscordId}` (Recherche effectuée en type String).");
                return;
            }

            await RespondAsync($"Joueur trouvé : {player.DiscordPseudo}");
        }
    }
}
