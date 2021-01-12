using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameException;
using GuildChat.Models.v1;
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

        // POST api/v1/message
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<Message>> Post(
            [FromServices] IMessageBusiness messageBusiness,
            [FromBody] Message message)
        {
            try
            {
                //Validar Player
                //Validar chat
                Message messageSaved = await messageBusiness.Save(message);
                return CreatedAtAction(null, messageSaved);
            }
            catch (ParameterException pe)
            {
                return BadRequest(pe.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/v1/message/5
        [HttpDelete("{id}/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> Delete(
            [FromServices] IMessageBusiness messageBusiness,
            string id, string playerId)
        {
            try
            {
                await messageBusiness.Delete(id, playerId);
                return NoContent();
            }
            catch (ParameterException pe)
            {
                return BadRequest(pe.Message);
            }
            catch (NotFoundException ne)
            {
                return NotFound(ne.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
