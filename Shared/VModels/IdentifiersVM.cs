using Shared.VModels;
using System;
using System.Collections.Generic;
using System.Text;
using Shared.VModels;

namespace Shared.VModels
{
    public class IdentifiersVM : BaseModelVM
    {
        public string? FiveMLicense { get; set; }
        public ulong DiscordLicense { get; set; }
        public string? SteamLicense { get; set; }

        public Guid PlayerID { get; set; }

        public PlayersVM Player { get; set; }
    }
}
