using System;
using System.Threading.Tasks;
using GameException;
using Microsoft.AspNetCore.Mvc;
using PlayerData.Business.v1;
using PlayerData.Models.v1;
using PlayerData.Services.v1;

namespace PlayerData.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlayerController : ControllerBase
    {
        // GET: api/v1/Player/5
        [HttpGet("{id}", Name = "Get")]
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/v1/Player
        [HttpPost]
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
