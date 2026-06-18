using ClassLibrary1.Models;
using FivemCsharpCore.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Identifiers : BaseModel
    {
        public required string? FiveMLicense { get; set; }
        public required string DiscordLicense { get; set; }
        public required string? SteamLicense { get; set; }

        public Guid PlayerID { get; set; }

        public required Players Player { get; set; }
    }
}
