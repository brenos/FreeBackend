using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.Business.v1;
using PersonalBudget.Models;

namespace PersonalBudget.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TransactionTypeController : ControllerBase
    {
        // GET: api/transactiontype
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TransactionType>>> GetAll(
            [FromServices]ITransactionTypeBO ttypeBO)
        {
            try
            {
                var ttypes = await ttypeBO.GetAll();
                return Ok(ttypes);
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

        // GET: api/transactiontype/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TransactionType>>> GetById(
            [FromServices]ITransactionTypeBO ttypeBO,
            string id)
        {
            try
            {
                var ttypes = await ttypeBO.GetById(id);
                return Ok(ttypes);
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

        // POST: api/transactiontype
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices]ITransactionTypeBO transactionTypeBO,
            [FromBody]TransactionType transactionType)
        {
            try
            {
                int result = await transactionTypeBO.Save(transactionType);
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

        // PUT: api/transactiontype/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromServices]ITransactionTypeBO transactionTypeBO,
            string id,
            [FromBody]TransactionType transactionType)
        {
            try
            {
                int result = await transactionTypeBO.Update(id, transactionType);
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

        // DELETE: api/transactiontype/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]ITransactionTypeBO transactionTypeBO,
            string id)
        {
            try
            {
                int result = await transactionTypeBO.Delete(id);
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