using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuildData.Models
{
    public class Guild
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Players { get; set; }
        public Guid Owner { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
