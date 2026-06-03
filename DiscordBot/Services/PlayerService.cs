using DiscordBot.Database;
using DiscordBot.ModelMongoose;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class PlayerService : IPlayerInterface
    {
        private readonly MongoContext _db;
        public PlayerService(MongoContext db)
        {
            _db = db;
        }
        public Task<PlayerInformations?> BanPlayerToDiscord(string DiscordID, string Reason, TimeSpan Duration, string? PseudoDiscord)
        {
            throw new NotImplementedException();
        }

        public async Task CreatePlayerByID(PlayerInformations player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            var CheckIfPlayerExist = await _db.Players.Find(p => p.DiscordID == player.DiscordID).FirstOrDefaultAsync();


            if (CheckIfPlayerExist != null)
            {
             
                var UpdateStatuDelete = await _db.Players.UpdateOneAsync(
                    Builders<PlayerInformations>.Filter.Eq(p => p.DiscordID, player.DiscordID),
                    Builders<PlayerInformations>.Update.Set(p => p.IsDeleted, false)
                );  

                return;
            }

            Console.WriteLine($"[MongoDB] Tentative de création du joueur : {player.DiscordPseudo} ({player.DiscordID})");
            //Faire une vérification de si l'id est déjà en DB ou pas 
            //PlayerInformations NewPlayer = new()
            //{
            //    DiscordID = player.DiscordID,
            //    DiscordName = player.DiscordName,
            //    DiscordPseudo = player.DiscordPseudo,
            //    AvatarUrl = player.AvatarUrl,
            //    Xp = 0,
            //    Level = 0,
            //    Grade = "Novice",
            //    CreatedAt = DateTime.UtcNow, // Corrigé en UtcNow
            //    UpdatedAt = DateTime.UtcNow, // Corrigé en UtcNow
            //    IsDeleted = false
            //};

            //await _db.Players.InsertOneAsync(NewPlayer);

            await _db.Players.InsertOneAsync(player);
            Console.WriteLine($"[MongoDB] Joueur {player.DiscordPseudo} ajouté avec succès !");
        }

        public Task<List<PlayerInformations>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerInformations?> GetPlayerByDiscordIDOrPseudoAsync(string playerInfo)
        {
            if (string.IsNullOrWhiteSpace(playerInfo))
                return null;

            var filter = Builders<PlayerInformations>.Filter.Or(
                Builders<PlayerInformations>.Filter.Eq(p => p.DiscordID, playerInfo),
                Builders<PlayerInformations>.Filter.Regex(p => p.DiscordPseudo, new BsonRegularExpression(playerInfo, "i")),
                Builders<PlayerInformations>.Filter.Regex(p => p.DiscordName, new BsonRegularExpression(playerInfo, "i"))
            );

            var player = await _db.Players.Find(filter).FirstOrDefaultAsync();

            if (player == null)
                Console.WriteLine($"[MongoDB] Aucun joueur trouvé pour : {playerInfo}");

            return player;
        }


        public async Task<PlayerInformations?> GetPlayerByDiscordByIDOrPseudoAsync(string playerInfo)
        {
            if (string.IsNullOrWhiteSpace(playerInfo))
                return null;

            var filter = Builders<PlayerInformations>.Filter.Or(
                Builders<PlayerInformations>.Filter.Eq(p => p.DiscordID, playerInfo),
                Builders<PlayerInformations>.Filter.Regex(p => p.DiscordPseudo, new BsonRegularExpression(playerInfo, "i")),
                Builders<PlayerInformations>.Filter.Regex(p => p.DiscordName, new BsonRegularExpression(playerInfo, "i"))
            );

            var player = await _db.Players.Find(filter).FirstOrDefaultAsync();

            if (player == null)
                Console.WriteLine($"[MongoDB] Aucun joueur trouvé pour : {playerInfo}");
            else
                Console.WriteLine($"[MongoDB] Joueur trouvé pour : {playerInfo}");

            return player;
        }

        public async Task<PlayerInformations?> GetPlayerByDiscordIDAsync(string discordId)
        {
            var player = await _db.Players
                .Find(p => p.DiscordID == discordId)
                .FirstOrDefaultAsync();

            if (player == null)
                Console.WriteLine($"[MongoDB] Aucun joueur trouvé avec DiscordID : {discordId}");
            else
                Console.WriteLine($"[MongoDB] Joueur trouvé avec DiscordID : {discordId}");

            return player;
        }

        public Task<PlayerInformations?> KickPlayerToDiscord(string DiscordID, string Reason, TimeSpan Duration)
        {
            throw new NotImplementedException();
        }

        public async Task SoftDeletePlayerID(string DiscordID)
        {
            var softDelete = await _db.Players.UpdateOneAsync(
                Builders<PlayerInformations>.Filter.Eq(p => p.DiscordID, DiscordID),
                Builders<PlayerInformations>.Update.Set(p => p.IsDeleted, true)
            );
        }

        public Task HardDeletePlayerID(string DiscordID)
        {
            var hardDelete = _db.Players.DeleteOneAsync(
                Builders<PlayerInformations>.Filter.Eq(p => p.DiscordID, DiscordID)
            );
            return hardDelete;
        }

        public Task<PlayerInformations?> UpdateLevelPlayerByIDAsync(string DiscordID, ushort NewLevel)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerInformations?> UpdateXpPlayerByIDAsync(string DiscordID, int XpToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
