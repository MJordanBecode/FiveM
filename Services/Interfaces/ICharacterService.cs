using Shared.DTOS;
using System;
using System.Threading.Tasks;
using static Shared.DTOS.SkinDataDto;

namespace Lostgen.Server.Services
{
    public interface ICharacterService
    {
        Task<bool> SaveCharacterCreationAsync(Guid playerId, string firstName, string lastName, DateTime birthDay, char gender, short height, FaceDataDto face, HairDataDto hair, ClothesDataDto clothes);

        Task<bool> HasCharacterAsync(Guid playerId);
    }
}