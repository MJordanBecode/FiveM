namespace DiscordBot.ModelMongoose
{
    public class Bans : BaseModel
    {
        public  string Reason { get; set; }
        public  DateTime BanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; } // null = permanent
    }
}
