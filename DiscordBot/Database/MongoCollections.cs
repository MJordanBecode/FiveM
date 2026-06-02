using DiscordBot.ModelMongoose;
using MongoDB.Driver;

namespace DiscordBot.Database;

public static class MongoCollections
{
    public static IMongoCollection<PlayerInformations> Players { get; set; } = null!;
}