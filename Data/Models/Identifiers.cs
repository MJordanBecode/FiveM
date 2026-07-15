using ClassLibrary1.Models;
using FivemCsharpCore.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Identifiers : BaseModel
    {
        public  string? FiveMLicense { get; set; }
        public  ulong DiscordLicense { get; set; }
        public  string? SteamLicense { get; set; }

        public Guid PlayerID { get; set; }

        public  Players Player { get; set; }
    }
}
