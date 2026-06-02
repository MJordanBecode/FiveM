using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBot.ModelMongoose
{
    public class PlayerInformations 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string DiscordName { get; set; }
        public required string DiscordPseudo { get; set; }
        [BsonElement("DiscordID")]public required string DiscordID { get; set; } // Changé en string
        public required string? AvatarUrl { get; set; }
        public required int Xp { get; set; } = 0;
        public required int Level { get; set; } = 0;
        public required string Grade { get; set; }
        public  DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public required bool IsDeleted { get; set; } = false;
    }
}
