using ClassLibrary1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FivemCsharpCore.Models
{
    public class PlayersVM : BaseModelVM
    {
        public  string FiveMLicense{ get; set; }
        public  string DiscordLicense { get; set; }
        public  string SteamLicense { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public DateTime BirthDay {  get; set; }
        public short Height { get; set; }
        public char Gender { get; set; }

        public  BankTransactionsVM Account { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
