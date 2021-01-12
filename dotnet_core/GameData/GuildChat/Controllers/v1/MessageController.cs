using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameException;
using GameModels.Mongo.v1;
using GuildChat.Business.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuildChat.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MessageController : ControllerBase
    {
        // GET api/v1/message/5
        [HttpGet("{guildId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<Message>>> Get(
            [FromServices] IMessageBusiness messageBusiness,
            string guildId)
        {
            try
            {
                IEnumerable<Message> messages = await messageBusiness.GetLastTweenty(guildId);
                return Ok(messages);
            }
            catch (ParameterException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
