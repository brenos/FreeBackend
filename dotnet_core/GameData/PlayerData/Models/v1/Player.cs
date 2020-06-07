using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PlayerData.Models.v1
{
    
    public class Player
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<LevelData> Levels { get; set; }
        public string GuildId { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class LevelData
    {
        public string Level { get; set; }
        public int Score { get; set; }
    }
}
