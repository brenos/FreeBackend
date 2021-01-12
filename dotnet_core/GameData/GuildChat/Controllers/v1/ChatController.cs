using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatClient.Chat.v1;
using GuildChat.Business.v1;
using GuildChat.Hubs.v1;
using GuildChat.Models.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuildChat.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChatController : ControllerBase
    {
        // POST api/chat/addtogroup
        [HttpPost("addtogroup")]
        public async Task<ActionResult> AddToGroup(
            [FromServices] IChatBusiness chatBusiness,
            [FromBody] Message message)
        {
            try
            {
                await chatBusiness.AddToGroup(message.PlayerId, message.GuildId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/chat/sendmessage
        [HttpPost("sendmessage")]
        public async Task<ActionResult> SendMessage(
            [FromServices] IChatBusiness chatBusiness,
            [FromBody] Message message)
        {
            try
            {
                await chatBusiness.SendMessage(message);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
