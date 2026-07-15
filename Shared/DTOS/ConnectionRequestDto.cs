using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOS
{
    public class ConnectionRequestDto
    {
        public string PlayerName { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;   // <-- Doit s'appeler "License"
        public string IpAddress { get; set; } = string.Empty; // <-- Doit s'appeler "IpAddress"
        public string DiscordId { get; set; } = string.Empty;
        public string SteamId { get; set; } = string.Empty;
    }
}
