namespace PlayerData.Models.v1
{
    public class PlayerDatabaseSettings : IPlayerDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPlayerDatabaseSettings
    {
        string PlayersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
