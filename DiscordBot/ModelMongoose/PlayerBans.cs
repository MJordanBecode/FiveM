using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class PlayerBans : BaseModel
    {
        public  string PlayerID { get; set; }
        public  string PlayerBanID { get; set; }

        [BsonIgnore]
        public Players? Players { get; set; }
        [BsonIgnore]
        public Bans? PlayerBan { get; set; }
    }
}
