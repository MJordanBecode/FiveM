using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Players : BaseModel
    {       
        public bool ConnectionOnce { get; set; } = false;
        public bool IsWhitelisted { get; set; } = false; // OFF => not acces to server

        public  Identifiers? Identifier { get; set; }
        public BankAccounts? BankAccount { get; set; }
        public ICollection<PlayerCharacters> Characters { get; set; }  // Personnages du joueur
            = new List<PlayerCharacters>();
    }
}
