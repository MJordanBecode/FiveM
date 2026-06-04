using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class PlayerInformationsKicks
    {
        public required string PlayerInformationsID { get; set; }
        public required string PlayerKickID { get; set; } = string.Empty;

        [BsonIgnore]
        public PlayerInformations? PlayerInformations { get; set; }
        [BsonIgnore]
        public PlayerKicks? Kicks { get; set; }
    }
}
