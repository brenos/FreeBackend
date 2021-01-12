using System;
using System.Threading.Tasks;
using GameException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerData.Business.v1;
using GameModels.Mongo.v1;

namespace PlayerData.Controllers.v1
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlayerController : ControllerBase
    {
        // GET: api/v1/Player/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<Player>> Get(
            [FromServices] IPlayerBusiness playerBusiness,
            string id)
        {
            try
            {
                Player player = await playerBusiness.GetById(id);
                return Ok(player);
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

        // POST: api/v1/Player
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Player))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Post(
            [FromServices] IPlayerBusiness playerBusiness,
            [FromBody] Player player)
        {
            try
            {
                Player playerSaved = await playerBusiness.Save(player);
                return CreatedAtAction(null, player);
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

        // PUT: api/v1/Player/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Put(
            [FromBody] Player player,
            [FromServices] IPlayerBusiness playerBusiness
        )
        {
            try
            {
                await playerBusiness.Update(player);
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

        // DELETE: api/v1/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(
            string id,
            [FromServices] IPlayerBusiness playerBusiness
        )
        {
            try
            {
                await playerBusiness.Delete(id);
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
