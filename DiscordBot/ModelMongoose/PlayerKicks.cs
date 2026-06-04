using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.ModelMongoose
{
    public class PlayerKicks : BaseModel
    {
        public required string Reason { get; set; }
        public required DateTime KickDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; } 
    }
}
