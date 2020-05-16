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
    public class TransactionController : ControllerBase
    {
        // GET: api/transaction/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetByUserId(
            [FromServices]ITransactionBO transactionBO,
            string userId)
        {
            try
            {
                var transactions = await transactionBO.GetByUserId(userId, 20);
                return Ok(transactions);
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

        // POST: api/transaction
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices]ITransactionBO transactionBO,
            [FromBody]Transaction transaction)
        {
            try
            {
                int result = await transactionBO.Save(transaction);
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

        // PUT: api/transaction/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromServices]ITransactionBO transactionBO,
            string id,
            [FromBody]Transaction transaction)
        {
            try
            {
                int result = await transactionBO.Update(id, transaction);
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

        // DELETE: api/transaction/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]ITransactionBO transactionBO,
            string id)
        {
            try
            {
                int result = await transactionBO.Delete(id);
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