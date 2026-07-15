using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public  string CreatedBy { get; set; }
    }
}
