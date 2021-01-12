using System;

namespace ChatClient.Models.v1
{
    public class Message
    {
        public string PlayerId { get; set; }
        public string GuildId { get; set; }
        public string Text { get; set; }
        public DateTime SendAt { get; set; }
    }
}
