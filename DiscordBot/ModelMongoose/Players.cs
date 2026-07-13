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
    public class Players : BaseModel
    {        
        public required string DiscordName { get; set; }
        public required string DiscordPseudo { get; set; }
        [BsonElement("DiscordID")]public required string DiscordID { get; set; } // Changé en string
        public required string? AvatarUrl { get; set; }
        public  int Xp { get; set; } = 0;
        public  int Level { get; set; } = 0;
        public required string Grade { get; set; }
        public  bool IsDeleted { get; set; } = false;
        public  bool IsBanned { get; set; } = false;
        public bool IsWhitelist { get; set; } = false;

    }
}
