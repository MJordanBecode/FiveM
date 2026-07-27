using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Players : BaseModel
    {
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public DateTime? BirthDay {  get; set; }
        public short? Height { get; set; }
        public char? Gender { get; set; }
        public bool ConnectionOnce { get; set; } = false;
        public bool IsWhitelisted { get; set; } = false; // OFF => not acces to server
        public Guid? SkinID { get; set; }

        public  BankAccounts? BankAccount { get; set; }
        public  Identifiers? Identifier { get; set; }
        public PlayerSkins? Skin { get; set; }



        public string FullName => $"{FirstName} {LastName}";

    }
}
