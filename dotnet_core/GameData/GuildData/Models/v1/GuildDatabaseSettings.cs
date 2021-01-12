namespace GuildData.Models.v1
{
    public class GuildDatabaseSettings : IGuildDatabaseSettings
    {
        public string GuildsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IGuildDatabaseSettings
    {
        string GuildsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
