using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.Business.v1;
using PersonalBudget.Models;

namespace PersonalBudget.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReleaseController : ControllerBase
    {
        // GET: api/re;ease/transactionId
        [HttpGet("{transactionId}")]
        public async Task<ActionResult<IEnumerable<Release>>> GetByTransactionId(
            [FromServices]IReleaseBO releaseBO,
            string transactionId)
        {
            try
            {
                var releases = await releaseBO.GetByTransactionId(transactionId);
                return Ok(releases);
            }
            catch (Exceptions.NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/release
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices]IReleaseBO releaseBO,
            [FromBody]Release release)
        {
            try
            {
                int result = await releaseBO.Save(release);
                if (result == 1)
                {
                    return CreatedAtAction(null, null);
                }
                else
                {
                    return UnprocessableEntity("Error on create");
                }
            }
            catch (DbException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/release/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromServices]IReleaseBO releaseBO,
            string id,
            [FromBody]Release release)
        {
            try
            {
                int result = await releaseBO.Update(id, release);
                if (result == 1)
                {
                    return NoContent();
                }
                else
                {
                    return UnprocessableEntity("Error on update");
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException e)
            {
                return NotFound(e.Message);
            }
            catch (DbException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exceptions.NotFoundException e)
            {
                return NotFound("Not Found");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/release/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]IReleaseBO releaseBO,
            string id)
        {
            try
            {
                int result = await releaseBO.Delete(id);
                if (result == 1)
                {
                    return NoContent();
                }
                else
                {
                    return UnprocessableEntity("Error on delete");
                }
            }
            catch (Exceptions.NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}