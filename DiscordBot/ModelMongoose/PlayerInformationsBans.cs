using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class PlayerInformationsBans : BaseModel
    {
        public required string PlayerInformationsID { get; set; }
        public required string PlayerBanID { get; set; }

        [BsonIgnore]
        public PlayerInformations? PlayerInformations { get; set; }
        [BsonIgnore]
        public PlayerBans? PlayerBan { get; set; }
    }
}
