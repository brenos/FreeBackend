namespace GuildChat.Models.v1
{
    public class GuildChatDatabaseSettings : IGuildChatDatabaseSettings
    {
        public string MessagesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IGuildChatDatabaseSettings
    {
        string MessagesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
