using Shared.VModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayersVM> CreatePlayerAsync(string license, string steamHex, string discordId, string playerName);
    }
}
