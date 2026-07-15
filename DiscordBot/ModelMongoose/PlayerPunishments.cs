using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.ModelMongoose
{
    public class PlayerPunishments : BaseModel
    {
        public  string PlayerID { get; set; }
        public  string PunishmentID { get; set; }

        [BsonIgnore]
        public Players? Player { get; set; }
        [BsonIgnore]
        public Punishments? Punishment { get; set; } = null;

    }
}
