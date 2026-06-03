namespace DiscordBot.ModelMongoose
{
    public class PlayerInformationsBan : BaseModel
    {
        public Guid PlayerInformationsID { get; set; }
        public Guid PlayerBanID { get; set; }

        public PlayerInformations? PlayerInformations { get; set; }
        public PlayerBan? PlayerBan { get; set; }
    }
}
