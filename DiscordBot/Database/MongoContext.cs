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

        public IMongoCollection<Players> Players =>
            Database.GetCollection<Players>("Players");
        public IMongoCollection<Bans> Bans =>
            Database.GetCollection<Bans>("Bans");

        public IMongoCollection<PlayerBans> PlayerBans => 
            Database.GetCollection<PlayerBans>("PlayerBans");

        public IMongoCollection<Punishments> Punishments => 
            Database.GetCollection<Punishments>("Punishments");

        public IMongoCollection<PlayerPunishments> PlayerPunishments => 
            Database.GetCollection<PlayerPunishments>("PlayerPunishments");

        public IMongoCollection<PlayerBans> PlayerInformationsBans =>
            Database.GetCollection<PlayerBans>("PlayerInformationsBans");

        public MongoContext(string connectionString)
        {
            var client = new MongoClient(connectionString);

            Database = client.GetDatabase("DrasdyukRPBot");
        }
    }
}
