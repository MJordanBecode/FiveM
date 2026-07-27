using Shared.VModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.VModels
{
    public class PlayersVM : BaseModelVM
    {
            public bool ConnectionOnce { get; set; } = false;
            public bool IsWhitelisted { get; set; } = false; // OFF => not acces to server
            public IdentifiersVM? Identifier { get; set; }
            public BankAccountsVM? BankAccount { get; set; }

        // Personnages du joueur    
        public ICollection<PlayerCharactersVM> Characters { get; set; } = new List<PlayerCharactersVM>();
    }
}
