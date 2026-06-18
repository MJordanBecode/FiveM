using ClassLibrary1.Models;
using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FivemCsharpCore.Models
{
    public class Players : BaseModel
    {
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public DateTime? BirthDay {  get; set; }
        public short? Height { get; set; }
        public char? Gender { get; set; }
        //public Guid IdentifierID { get; set; }

        public Guid BankAccountID { get; set; } //Bank
        public required BankAccounts BankAccount { get; set; }
        public required Identifiers? Identifier { get; set; }

        public bool ConnectionOnce { get; set; } = false;
        public bool IsWhitelisted { get; set; } = true; // OFF => not acces to server



        public string FullName => $"{FirstName} {LastName}";

    }
}
