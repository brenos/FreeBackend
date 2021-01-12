using MongoDB.Bson.Serialization.Attributes;

namespace GuildChat.Models.v1
{
    public class Message : ChatClient.Models.v1.Message
    {
        [BsonId]
        public string Id { get; set; }
    }
}
