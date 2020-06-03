using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuildChat.Models
{
    public class Chat
    {
        [BsonId]
        public string Id { get; set; }
        public string DateAndPlayerName { get; set; }
        public string Message { get; set; }
    }
}
