using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerData.Models;
using PlayerData.Services;

namespace PlayerData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // GET: api/Player
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Player/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Player
        [HttpPost]
        public void Post(
            [FromServices] IDynamoDB dynamoDB,
            [FromBody] Player player)
        {
            try
            {
                if (player != null)
                {
                    player.Id = Guid.NewGuid();
                    player.CreatedAt = DateTime.Now;
                    dynamoDB.Save(player);
                    Ok();
                }
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }
        }

        // PUT: api/Player/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
