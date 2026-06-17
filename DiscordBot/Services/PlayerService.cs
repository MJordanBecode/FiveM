using DiscordBot.Database;
using DiscordBot.ModelMongoose;
using MongoDB.Bson;
using MongoDB.Driver;
using DiscordBot.Enums;
using Discord;
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

        public async Task<Players?> BanPlayerToDiscordAsync(ulong discordId, string reason, DateTime? expiresAt)
        {
            if (string.IsNullOrWhiteSpace(reason) ||
                expiresAt == null ||
                expiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            var player = await GetPlayerByDiscordByIDAsync(discordId);

            if (player == null)
                return null;

            Bans ban = new()
            {
                Reason = reason,
                BanDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "LostGen",
            };

            await _db.Bans.InsertOneAsync(ban);

            PlayerBans playerBans = new()
            {
                PlayerID = player.Id,
                PlayerBanID = ban.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "LostGen",
            };

            await _db.PlayerBans.InsertOneAsync(playerBans);

            return player;
        }

        public async Task<bool> CheckIfPlayerIsBanned(ulong discordId)
        {
            var player = await GetPlayerByDiscordByIDAsync(discordId);

            if (player == null)
                return false;

            return await _db.PlayerBans
                .Find(b => b.PlayerID == player.Id)
                .AnyAsync();
        }
        public async Task<Punishments?> WarnPlayerByDiscordIDAsync(ulong discordId, string reason, PunishmentTypes punishmentTypes)
        {
            var player = await GetPlayerByDiscordByIDAsync(discordId);

            if (player == null)
                return null;

            Punishments warn = new()
            {
                Reason = reason,
                Type = punishmentTypes.ToString(),
                Duration = null,
                PlayerID = player.Id,
                CreatedBy = "LostGen",
                CreatedAt = DateTime.UtcNow,
            };

            await _db.Punishments.InsertOneAsync(warn);

            return warn;
        }

        public async Task<int> PunishmentPlayerCount(ulong discordId, PunishmentTypes punishmentType)
        {
            var player = await GetPlayerByDiscordByIDAsync(discordId);

            if (player == null)
                return 0;

            return (int)await _db.Punishments.CountDocumentsAsync(
                p => p.PlayerID == player.Id &&
                     p.Type == punishmentType.ToString()
            );
        }


        public async Task CreatePlayerByID(Players player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            var CheckIfPlayerExist = await _db.Players.Find(p => p.DiscordID == player.DiscordID).FirstOrDefaultAsync();


            if (CheckIfPlayerExist != null)
            {
             
                var UpdateStatuDelete = await _db.Players.UpdateOneAsync(
                    Builders<Players>.Filter.Eq(p => p.DiscordID, player.DiscordID),
                    Builders<Players>.Update.Set(p => p.IsDeleted, false)
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

        public Task<List<Players>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<Players?> GetPlayerByDiscordByIDAsync(ulong discordId)
        {
            var discordIdString = discordId.ToString();

            var player = await _db.Players
                .Find(p => p.DiscordID == discordIdString)
                .FirstOrDefaultAsync();

            if (player != null)
                Console.WriteLine($"[MongoDB] Joueur trouvé avec DiscordID : {discordIdString}");
            else
                Console.WriteLine($"[MongoDB] Aucun joueur trouvé avec DiscordID : {discordIdString}");

            return player;
        }


        public Task<Players?> KickPlayerToDiscord(ulong discordId, string reason, DateTime? expiresAt)
        {
            throw new NotImplementedException();
        }

        public Task<Players?> MutePlayerToDiscord(ulong discordId, string reason, DateTime? expiresAt)
        {
            throw new NotImplementedException();
        }

        public async Task SoftDeletePlayerID(ulong discordId)
        {
            await _db.Players.UpdateOneAsync(
                Builders<Players>.Filter.Eq(p => p.DiscordID, discordId.ToString()),
                Builders<Players>.Update.Set(p => p.IsDeleted, true)
            );
        }

        public Task HardDeletePlayerID(ulong discordId)
        {
            return _db.Players.DeleteOneAsync(
                Builders<Players>.Filter.Eq(p => p.DiscordID, discordId.ToString())
            );
        }

        public Task<Players?> UpdateLevelPlayerByIDAsync(ulong discordId, ushort newLevel)
        {
            throw new NotImplementedException();
        }

        public Task<Players?> UpdateXpPlayerByIDAsync(ulong discordId, int xpToAdd)
        {
            throw new NotImplementedException();
        }




    }
}
