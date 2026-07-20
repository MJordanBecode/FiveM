using Data.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Services
{
    public class FiveMPlayerService : IFivemPlayerInterfaces
    {
        private readonly ApplicationDbContext _context;

        public FiveMPlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Players> CreatePlayerFivemAsync()
        {
            Players players = new Players
            {
                FirstName = null,
                LastName = null,
                BirthDay = null,
                Height = null,
                Gender = null,
                BankAccount = null,
                ConnectionOnce = false,
                IsWhitelisted = true,

                Identifier = null,
                CreatedBy = "LostgenRP",
            };

            await _context.Players.AddAsync(players);

            return players;
        }

        public async Task<Identifiers> CreateIdentifiersFivemAsync(ulong DiscordId, Players player)
        {
            Identifiers identifiers = new()
            {
                FiveMLicense = null,
                DiscordLicense = DiscordId,
                SteamLicense = null,
                Player = player,
                CreatedBy = "LostgenRP"
            };

            await _context.Identifiers.AddAsync(identifiers);

            return identifiers;
        }
    }
}
