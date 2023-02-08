using ChatWebApi.Models;
using ChatWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMsgService msgService;

        public MessagesController(IMsgService msgService)
        {
            this.msgService = msgService;
        }


        [HttpPost("addMessage")]
        public IActionResult AddMessageToDB(Message msg)
        {
            msgService.AddMessageToDB(msg);
            return Ok();
        }


        [HttpPost("loadMessages")]
        public ActionResult<IEnumerable<Message>> LoadMessagesFromDB(object[] room)
        {
            var messages = msgService.LoadMessagesFromDB(room[0].ToString()!);
            return Ok(messages);
        }

    }
}
