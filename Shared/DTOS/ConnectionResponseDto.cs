using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOS
{
    public class ConnectionResponseDto
    {
        public bool IsAllowed { get; set; }
        public string RejectReason { get; set; }
        public int UserId { get; set; } // L'ID unique de la base de données
        public string Role { get; set; } // Ex: "Player", "Admin", "SuperAdmin"
    }
}
