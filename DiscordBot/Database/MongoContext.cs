using MongoDB.Driver;
using DiscordBot.ModelMongoose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Database
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<PlayerInformations> Players =>
            Database.GetCollection<PlayerInformations>("PlayerInformations");
        public IMongoCollection<PlayerBans> Bans =>
            Database.GetCollection<PlayerBans>("PlayerBans");
        public IMongoCollection<PlayerInformationsBans> PlayerInformationsBans =>
            Database.GetCollection<PlayerInformationsBans>("PlayerInformationsBans");

        public MongoContext(string connectionString)
        {
            var client = new MongoClient(connectionString);

            Database = client.GetDatabase("DrasdyukRPBot");
        }
    }
}
