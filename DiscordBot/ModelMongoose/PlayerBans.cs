using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class PlayerBans : BaseModel
    {
        public required string PlayerID { get; set; }
        public required string PlayerBanID { get; set; }

        [BsonIgnore]
        public Players? Players { get; set; }
        [BsonIgnore]
        public Bans? PlayerBan { get; set; }
    }
}
