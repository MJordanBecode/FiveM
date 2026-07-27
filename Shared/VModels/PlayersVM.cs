using Shared.VModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.VModels
{
    public class PlayersVM : BaseModelVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public short? Height { get; set; }
        public char? Gender { get; set; }
        public bool ConnectionOnce { get; set; } = false;
        public bool IsWhitelisted { get; set; } = false; // OFF => not acces to server
        public Guid? IdentifierID { get; set; }
        public Guid? SkinID { get; set; }

        public Guid? BankAccountID { get; set; } //Bank
        public BankAccountsVM? BankAccount { get; set; }
        public IdentifiersVM? Identifier { get; set; }
        public PlayerSkinsVM? Skin { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
