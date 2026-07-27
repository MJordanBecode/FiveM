using Data.Context;
using Data.Models;
using Lostgen.Server.Services;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Shared.DTOS;
using System;
using System.Text.Json;
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

        public async Task<bool> SaveCharacterCreationAsync(Guid playerId, string firstName, string lastName, DateTime birthDay, char gender, short height, FaceDataDto face, HairDataDto hair, ClothesDataDto clothes)
        {
            // 1. Récupérer le joueur avec son skin associé
            var player = await _dbContext.Players
                .Include(p => p.Skin)
                .FirstOrDefaultAsync(p => p.ID == playerId);

            if (player == null) return false;

            // 2. Mettre à jour les données civiles
            player.FirstName = firstName;
            player.LastName = lastName;
            player.BirthDay = birthDay;
            player.Gender = gender;
            player.Height = height;
            player.ConnectionOnce = true;

            // 3. Traiter le skin
            var skin = player.Skin ?? new PlayerSkins { ID = Guid.NewGuid() };
            skin.Face = JsonSerializer.Serialize(face);
            skin.Hair = JsonSerializer.Serialize(hair);
            skin.Clothes = JsonSerializer.Serialize(clothes);
            skin.Props = "{}";
            skin.Overlays = "{}";

            if (player.Skin == null)
            {
                player.Skin = skin;
                player.SkinID = skin.ID;
            }

            // 4. Sauvegarder en BDD
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasCharacterAsync(Guid playerId)
        {
            return await _dbContext.Players
                .AnyAsync(p => p.ID == playerId && p.ConnectionOnce);
        }
    }
}