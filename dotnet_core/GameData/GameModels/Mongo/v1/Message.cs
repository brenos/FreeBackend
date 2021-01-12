using MongoDB.Bson.Serialization.Attributes;

namespace GameModels.Mongo.v1
{
    public class Message
    {
        [BsonId]
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string GuildId { get; set; }
        public string Text { get; set; }
        public System.DateTime SendAt { get; set; }
    }
}
