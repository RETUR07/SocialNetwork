using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System.Collections.Generic;

namespace SocialNetwork.Worker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("chats/{userId}")]
        public IActionResult GetChats(int userId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new{ RequestType = 0, UserId = userId }));
            return Ok();
        }

        [HttpGet("messages/{chatId}")]
        public IActionResult GetMessages(int chatId)
        {
            var data = new List<object>();
            data.Add(chatId);
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 1, ChatId = chatId }));
            return Ok();
        }

        [HttpGet("message/{messageId}")]
        public IActionResult GetMessage(int messageId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 2, MessageId = messageId }));
            return Ok();
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public IActionResult GetChat(int chatId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 3, ChatId = chatId }));
            return Ok();
        }

        [HttpDelete("chat/{chatId}")]
        public IActionResult DeleteChat(int chatId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 4, ChatId = chatId }));
            return Ok();
        }

        [HttpDelete("message/{messageId}")]
        public IActionResult DeleteMessage(int messageId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 5, MessageId = messageId }));
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] ChatForm chatdto)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 6, Chatdto = chatdto }));
            return Ok();
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public IActionResult AddUser(int chatId, int userId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 7, ChatId = chatId, UserId = userId }));
            return Ok();
        }

        [HttpPut("addmessage/{chatId}")]
        public IActionResult AddMessage([FromForm] MessageForm messagedto, int chatId)
        {
            _workerService.Enqueue(JsonConvert.SerializeObject(new { RequestType = 8, Messagedto = messagedto, ChatId = chatId }));
            return Ok();
        }
    }       
}
