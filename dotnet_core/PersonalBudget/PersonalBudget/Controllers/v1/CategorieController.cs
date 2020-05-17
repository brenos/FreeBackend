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
    public class CategorieController : ControllerBase
    {
        // GET: api/Categorie/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetByUserId(
            [FromServices]ICategorieBO categorieBO,
            string userId)
        {
            try
            {
                var categories = await categorieBO.GetByUserId(userId);
                return Ok(categories);
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

        // POST: api/Categorie
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices]ICategorieBO categorieBO,
            [FromBody]Categorie categorie)
        {
            try
            {
                int result = await categorieBO.Save(categorie);
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

        // PUT: api/Categorie/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromServices]ICategorieBO categorieBO,
            string id,
            [FromBody]Categorie categorie)
        {
            try
            {
                int result = await categorieBO.Update(id, categorie);
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

        // DELETE: api/ApiWithActions/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]ICategorieBO categorieBO,
            string id)
        {
            try
            {
                int result = await categorieBO.Delete(id);
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
