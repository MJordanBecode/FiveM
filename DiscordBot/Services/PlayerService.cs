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
using DiscordBot.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Services
{
    public class PlayerService : IPlayerInterface
    {
        private readonly MongoContext _db;
        private readonly ApplicationDbContext _context;
        private readonly IFivemPlayerInterfaces _fivemService;
        public PlayerService(MongoContext db, ApplicationDbContext Context, IFivemPlayerInterfaces fivemService)
        {
            _db = db;
            _context = Context;
            _fivemService = fivemService; 
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
                .Find(p => p.DiscordID == discordIdString.ToString())
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

        public async Task<Players?> WhiteListPlayer(ulong DiscordId)
        {
            if (DiscordId == 0) return null;

            // 1. On cherche le joueur dans MongoDB
            var MonogoPlayerExist = await GetPlayerByDiscordByIDAsync(DiscordId);

            // CAS 1 : Le joueur n'existe vraiment pas dans MongoDB
            if (MonogoPlayerExist == null)
            {
                Console.WriteLine($"[Whitelist] Échec : Aucun compte Discord trouvé dans MongoDB pour l'ID {DiscordId}.");
                return null;
            }

            // CAS 2 : Le joueur existe MAIS il est déjà Whitelisté
            if (MonogoPlayerExist.IsWhitelist == true)
            {
                Console.WriteLine($"[Whitelist] Info : Le joueur {DiscordId} est déjà whitelisté dans MongoDB.");
                // On renvoie quand même le joueur pour indiquer au bouton que le compte existe !
                return MonogoPlayerExist;
            }

            // CAS 3 : Le joueur existe et n'est pas encore Whitelisté (Traitement normal)
            Console.WriteLine($"[Whitelist] En cours pour le joueur {DiscordId}...");

            // Mise à jour MongoDB
            await _db.Players.UpdateOneAsync(
                Builders<Players>.Filter.Eq(p => p.DiscordID, DiscordId.ToString()),
                Builders<Players>.Update.Set(p => p.IsWhitelist, true)
            );

            // 🌟 CORRECTION MYSQL : On convertit en string (ou $"discord:{DiscordId}" selon ton format FiveM)
            string discordIdStr = DiscordId.ToString();

            var mysqlIdentifier = await _context.Identifiers
                .Include(i => i.Player)
                .FirstOrDefaultAsync(i => i.DiscordLicense == ulong.Parse(discordIdStr)); // Utilisation de la string ici

            if (mysqlIdentifier == null)
            {
                Console.WriteLine($"[MySQL] Aucun compte FiveM trouvé pour {DiscordId}. Création en cours...");
                var newFivemPlayer = await _fivemService.CreatePlayerFivemAsync();
                await _fivemService.CreateIdentifiersFivemAsync(DiscordId, newFivemPlayer);
            }
            else
            {
                if (mysqlIdentifier.Player != null && !mysqlIdentifier.Player.IsWhitelisted)
                {
                    Console.WriteLine($"[MySQL] Compte FiveM existant trouvé pour {DiscordId}. Activation de la Whitelist...");
                    mysqlIdentifier.Player.IsWhitelisted = true;
                }
            }

            await _context.SaveChangesAsync();

            // On met à jour l'objet local avant de le renvoyer pour que le bot sache qu'il vient d'être activé
            MonogoPlayerExist.IsWhitelist = true;
            return MonogoPlayerExist;
        }

        public Task<bool> CheckIfPlayerIsWhitelisted(ulong discordId)
        {
            throw new NotImplementedException();
        }




    }
}
