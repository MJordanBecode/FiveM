using DiscordBot.Database;
using DiscordBot.Interfaces;
using DiscordBot.ModelMongoose;
using DiscordBot.Services;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Reflection;

Env.TraversePath().Load();

var discordToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
    ?? throw new InvalidOperationException("DISCORD_TOKEN introuvable");

var mongoDbUserName = Environment.GetEnvironmentVariable("MongoDBUserName")
    ?? throw new InvalidOperationException("MongoDBUserName introuvable");

var mongoDbPassword = Environment.GetEnvironmentVariable("MongoDBPassword")
    ?? throw new InvalidOperationException("MongoDBPassword introuvable");

var guildId = ulong.Parse(
    Environment.GetEnvironmentVariable("DISCORD_GUILD")
    ?? throw new InvalidOperationException("DISCORD_GUILD introuvable")
);

string connectionUri = $"mongodb+srv://{mongoDbUserName}:{mongoDbPassword}@cluster0.velxzps.mongodb.net/?appName=Cluster0";
var mongoContext = new MongoContext(connectionUri);

var count = await mongoContext.Players.CountDocumentsAsync(Builders<PlayerInformations>.Filter.Empty);
Console.WriteLine($"[MongoDB] Connexion OK — {count} joueurs en base.");

var services = new ServiceCollection()
    .AddSingleton(mongoContext)
    .AddSingleton<IPlayerInterface, PlayerService>()
    .BuildServiceProvider();

var playerService = services.GetRequiredService<IPlayerInterface>();

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers
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

    await interactions.AddModulesAsync(Assembly.GetExecutingAssembly(), services);
    await interactions.RegisterCommandsToGuildAsync(guildId);

    Console.WriteLine("Slash commands enregistrées.");

    client.UserJoined += async user =>
    {
        Console.WriteLine($"[DEBUG] Quelqu'un rejoint : {user.Username}");

        PlayerInformations newPlayer = new()
        {
            CreatedBy = "system",
            DiscordID = user.Id.ToString(),
            DiscordName = user.Username,
            DiscordPseudo = user.GlobalName ?? user.Username,
            AvatarUrl = user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl(),
            Xp = 0,
            Level = 0,
            Grade = "Novice",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
            IsBanned = false
        };

        await playerService.CreatePlayerByID(newPlayer);
    };

    client.UserLeft += async (guild, user) =>
    {
        Console.WriteLine($"[DEBUG] Quelqu'un quitte : {user.Username} (ID: {user.Id})");

        var player = await playerService.GetPlayerByDiscordIDAsync(user.Id.ToString());
        if (player == null) return;

        TimeSpan tempsPasseSurLeServeur = DateTime.UtcNow - player.CreatedAt;

        if (tempsPasseSurLeServeur < TimeSpan.FromHours(2))
        {
            Console.WriteLine("[DEBUG] Joueur éphémère. Suppression définitive de la BDD.");
            await playerService.HardDeletePlayerID(user.Id.ToString());
        }
        else
        {
            Console.WriteLine("[DEBUG] Joueur régulier. Archivage du profil.");
            await playerService.SoftDeletePlayerID(user.Id.ToString());
        }
    };

    Console.WriteLine("Écoute des événements membres activée !");
};

client.InteractionCreated += async interaction =>
{
    var context = new SocketInteractionContext(client, interaction);
    await interactions.ExecuteCommandAsync(context, services);
};

await client.LoginAsync(TokenType.Bot, discordToken);
await client.StartAsync();

await Task.Delay(-1);