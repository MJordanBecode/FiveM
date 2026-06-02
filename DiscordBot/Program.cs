using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using DotNetEnv;
using System.Reflection;
using DiscordBot.Database;
using DiscordBot.ModelMongoose;
using MongoDB.Driver;
using MongoDB.Bson;


Env.TraversePath().Load();

var discordToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
var mongoDbUserName = Environment.GetEnvironmentVariable("MongoDBUserName");
var mongoDbPassword = Environment.GetEnvironmentVariable("MongoDBPassword");

if (string.IsNullOrWhiteSpace(mongoDbUserName) || string.IsNullOrWhiteSpace(mongoDbPassword))
{
    throw new Exception("MongoDBUserName ou MongoDBPassword introuvable");
}

string connectionUri = $"mongodb+srv://{mongoDbUserName}:{mongoDbPassword}@cluster0.velxzps.mongodb.net/?appName=Cluster0";
var mongoContext = new MongoContext(connectionUri);

MongoCollections.Players = mongoContext.Database
    .GetCollection<PlayerInformations>("PlayerInformations");

var count = await MongoCollections.Players.CountDocumentsAsync(_ => true);
Console.WriteLine($"Documents trouvés : {count}");

if (string.IsNullOrWhiteSpace(connectionUri))
{
    throw new Exception("URI de connexion MongoDB introuvable");
}


if (string.IsNullOrWhiteSpace(discordToken))
{
    throw new Exception("DISCORD_TOKEN introuvable");
}

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.Guilds
});

var interactions = new InteractionService(client.Rest);

client.Log += message =>
{
    Console.WriteLine(message.ToString());
    return Task.CompletedTask;
};

interactions.Log += message =>
{
    Console.WriteLine(message.ToString());
    return Task.CompletedTask;
};

client.Ready += async () =>
{
    Console.WriteLine($"Connecté en tant que {client.CurrentUser}");

    ulong guildId = 1398732675145011271;

    await interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), null);
    await interactions.RegisterCommandsToGuildAsync(guildId);

    Console.WriteLine("Slash commands enregistrées");
};

client.InteractionCreated += async interaction =>
{
    var context = new SocketInteractionContext(client, interaction);
    await interactions.ExecuteCommandAsync(context, null);
};
Console.WriteLine(MongoCollections.Players == null
    ? "Players NULL"
    : "Players OK");

await client.LoginAsync(TokenType.Bot, discordToken);
await client.StartAsync();

await Task.Delay(-1);