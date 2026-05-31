using ClassLibrary1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FivemCsharpCore.Models
{
    public class Players : BaseModel
    {
        public required string FiveMLicense{ get; set; }
        public required string DiscordLicense { get; set; }
        public required string SteamLicense { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDay {  get; set; }
        public short Height { get; set; }
        public char Gender { get; set; }

        public Guid AccountID { get; set; }
        public required BankTransactions Account { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
