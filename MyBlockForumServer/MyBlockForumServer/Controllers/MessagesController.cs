using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.Controllers.RequestProceccor;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Services;

namespace MyBlockForumServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessagesController(IMessageService messageService) : Controller
    {
        private readonly IMessageService _messageService = messageService;

        [HttpGet("MessagesByUser/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesByUserAsync(Guid id)
        {
            return await RequestHandler<IEnumerable<MessageDto>>.
                ProcessingGetByGuidRequest(this, _messageService.GetAllMessagesByUserAsync, id);
        }

        [HttpGet("ThreadMessages/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesFromThreadAsync(Guid id)
        {
            return await RequestHandler<IEnumerable<MessageDto>>.
                ProcessingGetByGuidRequest(this, _messageService.GetAllMessagesFromThreadAsync, id);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessageAsync(MessageDto message)
        {
            if (message == null) { return BadRequest(); }
            return await _messageService.SendMessageAsync(message);
        }

        [HttpPut]
        public async Task<ActionResult<Message>> EditMessageAsync(MessageDto message)
        {
            if (message == null) { return BadRequest(); }
            await _messageService.EditMessageAsync(message);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Ok();
        }
    }
}
