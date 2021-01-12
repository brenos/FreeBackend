using GameModels.Mongo.v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.v1
{
    public class GuildChatService : IGuildChatService
    {
        string uri = "http://localhost:54965";

        public async Task<List<Message>> GetLastMessages(string guildId)
        {
            List<Message> messages = new List<Message>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{uri}/api/v1/message/{guildId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    messages = JsonConvert.DeserializeObject<List<Message>>(apiResponse);
                }
            }
            return messages;
        }
    }
}
