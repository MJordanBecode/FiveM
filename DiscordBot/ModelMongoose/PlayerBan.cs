namespace DiscordBot.ModelMongoose
{
    public class PlayerBan : BaseModel
    {
        public required string Reason { get; set; }
        public required Timer BanDuration { get; set; }
    }
}
