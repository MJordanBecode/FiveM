using Data.Context;
using Data.Models;
using Lostgen.Server.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Interfaces;
using Shared.DTOS;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static Shared.DTOS.SkinDataDto;

namespace Services.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ApplicationDbContext _dbContext;

        public CharacterService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> SaveCharacterCreationAsync(
            Guid playerId,
            string firstName,
            string lastName,
            DateTime birthDay,
            char gender,
            short height,
            FaceDataDto face,
            HairDataDto hair,
            ClothesDataDto clothes)
        {
            // Récupération du compte joueur
            var player = await _dbContext.Players
                .FirstOrDefaultAsync(p => p.ID == playerId);


            if (player == null)
            {
                Debug.WriteLine(
                    $"[CharacterService] Joueur introuvable : {playerId}"
                );

                return false;
            }


            Debug.WriteLine("========== PLAYER ==========");
            Debug.WriteLine($"Player ID : {player.ID}");
            Debug.WriteLine("============================");



            // Vérifie si un personnage existe déjà
            var existingCharacter = await _dbContext.PlayerCharacters
                .AnyAsync(c => c.PlayerID == player.ID);


            if (existingCharacter)
            {
                Debug.WriteLine(
                    "[CharacterService] Le joueur possède déjà un personnage."
                );

                return false;
            }



            // Création du skin
            var skinId = Guid.NewGuid();

            var skin = new PlayerSkins
            {
                ID = skinId,

                Face = JsonConvert.SerializeObject(face),
                Hair = JsonConvert.SerializeObject(hair),
                Clothes = JsonConvert.SerializeObject(clothes),

                Props = "{}",
                Overlays = "{}",

                CreatedAt = DateTime.UtcNow,
                CreatedBy = "LostgenRP"
            };



            // Création du personnage
            var character = new PlayerCharacters
            {
                ID = Guid.NewGuid(),

                PlayerID = player.ID,

                SkinID = skinId,

                FirstName = firstName,
                LastName = lastName,

                BirthDay = birthDay,
                Gender = gender,
                Height = height,

                Skin = skin
            };



            // Le compte a déjà été connecté
            player.ConnectionOnce = true;



            await _dbContext.PlayerSkins.AddAsync(skin);

            await _dbContext.PlayerCharacters.AddAsync(character);



            Debug.WriteLine("========== CHANGE TRACKER ==========");

            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                Debug.WriteLine(
                    $"{entry.Entity.GetType().Name} => {entry.State}"
                );
            }

            Debug.WriteLine("====================================");



            try
            {
                Debug.WriteLine("[CharacterService] Avant SaveChanges");

                await _dbContext.SaveChangesAsync();

                Debug.WriteLine("[CharacterService] Après SaveChanges");

                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(
                    "========== CONCURRENCY EXCEPTION =========="
                );

                foreach (var entry in ex.Entries)
                {
                    Debug.WriteLine(
                        $"Entity : {entry.Entity.GetType().Name} | State : {entry.State}"
                    );
                }

                Debug.WriteLine(ex);

                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "========== SAVE EXCEPTION =========="
                );

                Debug.WriteLine(ex);

                throw;
            }
        }



        public async Task<bool> HasCharacterAsync(Guid playerId)
        {
            return await _dbContext.PlayerCharacters
                .AnyAsync(c => c.PlayerID == playerId);
        }

        public async Task<bool> UpdateCharacterPositionAsync(Guid characterId, float x, float y, float z, float heading)
        {
            var character = await _dbContext.PlayerCharacters
                .FirstOrDefaultAsync(c => c.ID == characterId);

            if (character == null)
            {
                Debug.WriteLine($"[CharacterService] Personnage introuvable : {characterId}");
                return false;
            }

            character.PositionX = x;
            character.PositionY = y;
            character.PositionZ = z;
            character.Heading = heading;

            try
            {
                await _dbContext.SaveChangesAsync();
                Debug.WriteLine($"[CharacterService] Position sauvegardée pour {characterId} : ({x}, {y}, {z})");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CharacterService] Erreur sauvegarde position : {ex}");
                return false;
            }
        }
    }
}