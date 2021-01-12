using GameModels.Mongo.v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace GameServices.v1
{
    public class PlayerService : IPlayerService
    {
        string uri = "http://localhost:54953";
        public async Task<Player> GetById(string playerId)
        {
            Player player = null;
            var handler = new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
            };
            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.GetAsync($"{uri}/api/v1/player/{playerId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    player = JsonConvert.DeserializeObject<Player>(apiResponse);
                }
            }
            return player;
        }
    }
}
