using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GuildData.Models.v1
{
    public class Guild
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Players { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GuildAddPlayer
    {
        [BsonId]
        public string Id { get; set; }
        public string PlayerId { get; set; }
    }
}
