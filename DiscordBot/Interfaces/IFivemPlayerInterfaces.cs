using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;
using Data.Models;
using FivemCsharpCore.Models;

namespace DiscordBot.Interfaces
{
    public interface IFivemPlayerInterfaces
    {
        public Task<Players> CreatePlayerFivemAsync();


        public Task<Identifiers> CreateIdentifiersFivemAsync(ulong DiscordId, Players player);

    }
}
