namespace DiscordBot.ModelMongoose
{
    public class PlayerBans : BaseModel
    {
        public required string Reason { get; set; }
        public required DateTime BanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; } // null = permanent
    }
}
