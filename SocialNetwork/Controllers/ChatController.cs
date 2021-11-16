using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("chats/{userId}")]
        public async Task<IActionResult> GetChats(int userId)
        {
            var chats = await _chatService.GetChats(userId);
            if (chats == null) return NotFound();
            return Ok(chats);
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _chatService.GetMessages(chatId);
            if (messages == null) return NotFound();
            return Ok(messages);
        }

        [HttpGet("message/{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int messageId)
        {
            var message = await _chatService.GetMessage(messageId);
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            var chat = await _chatService.GetChat(chatId);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [HttpDelete("chat/{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            await _chatService.DeleteChat(chatId);
            return NoContent();
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            await _chatService.DeleteMessage(messageId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatForm chatdto)
        {
            var chat = await _chatService.CreateChat(chatdto);
            if (chat == null)
            {
                return BadRequest("Chat is null");
            }
            return CreatedAtRoute("GetChat", new { chatId = chat.Id }, await _chatService.GetChat(chat.Id));
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public async Task<IActionResult> AddUser(int chatId, int userId)
        {
            var success = await _chatService.AddUser(chatId, userId);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("addmessage{chatId}")]
        public async Task<IActionResult> AddMessage([FromBody]MessageForm messagedto, int chatId)
        {
            var success = await _chatService.AddMessage(chatId, messagedto);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
