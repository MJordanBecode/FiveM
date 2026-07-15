using MongoDB.Bson.Serialization.Attributes;
namespace DiscordBot.ModelMongoose
{
    public class Punishments : BaseModel
    {
        public  string Type { get; set; }
        public  string Reason { get; set; }
        public TimeSpan? Duration { get; set; } // en fonction du type, on peut lui mettre une durée ou pas (null = permanent)

        public  string PlayerID { get; set; }

        [BsonIgnore]
        public Players? Player { get; set; }
    }
}
