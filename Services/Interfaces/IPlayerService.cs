using Shared.VModels;
using System;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayersVM> CreatePlayerAsync(
            string license,
            string steamHex,
            ulong discordId
        );

        Task<PlayersVM> GetPlayerByLicenseAsync(
            string license
        );

        Task<PlayerCharactersVM?> GetCharacterAsync(
            Guid playerId
        );

        Task<bool> HasCharacterAsync(Guid playerId);
    }
}