using System;
using System.Threading.Tasks;
using GameException;
using Microsoft.AspNetCore.Mvc;
using GuildData.Business.v1;
using GuildData.Models.v1;
using Microsoft.AspNetCore.Http;

namespace GuildData.Controllers.v1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GuildController : ControllerBase
    {
        // GET: api/v1/Guild/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<Guild>> Get(
            [FromServices] IGuildBusiness guildBusiness,
            string id)
        {
            try
            {
                Guild guild = await guildBusiness.GetById(id);
                return Ok(guild);
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

        // POST: api/v1/Guild
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guild))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Post(
            [FromServices] IGuildBusiness guildBusiness,
            [FromBody] Guild guild)
        {
            try
            {
                Guild guildSaved = await guildBusiness.Save(guild);
                return CreatedAtAction(null, guild);
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

        // PUT: api/v1/Guild/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Put(
            [FromBody] Guild guild,
            [FromServices] IGuildBusiness guildBusiness
        )
        {
            try
            {
                await guildBusiness.Update(guild);
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

        // PUT: api/v1/Guild/addPlayer
        [HttpPut("addPlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> AddPlayer(
            [FromBody] GuildAddPlayer guildAddPlayer,
            [FromServices] IGuildBusiness guildBusiness
        )
        {
            try
            {
                await guildBusiness.AddPlayerInGuild(guildAddPlayer);
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

        // DELETE: api/v1/Guild/5
        [HttpDelete("{id}/{ownerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(
            string id, string ownerId,
            [FromServices] IGuildBusiness guildBusiness
        )
        {
            try
            {
                await guildBusiness.Delete(id, ownerId);
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
