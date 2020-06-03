using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PlayerData.Models
{
    
    public class Player
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Levels { get; set; }
        public Guid GuildId { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class LevelData
    {
        public string Level { get; set; }
        public int Score { get; set; }
    }
}
