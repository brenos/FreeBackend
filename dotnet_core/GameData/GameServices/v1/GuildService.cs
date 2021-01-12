using GameModels.Mongo.v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameServices.v1
{
    public class GuildService : IGuildService
    {
        string uri = "http://localhost:54964";

        public async Task<Guild> GetById(string guildId)
        {
            Guild guild = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{uri}/api/v1/guild/{guildId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    guild = JsonConvert.DeserializeObject<Guild>(apiResponse);
                }
            }
            return guild;
        }
    }
}
