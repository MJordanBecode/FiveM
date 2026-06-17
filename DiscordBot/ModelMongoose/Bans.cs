namespace DiscordBot.ModelMongoose
{
    public class Bans : BaseModel
    {
        public required string Reason { get; set; }
        public required DateTime BanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; } // null = permanent
    }
}
