using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var data = new List<object>();
            data.Add(userId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 0, Data = data }));
            return Ok();
        }

        [HttpGet("messages/{chatId}")]
        public IActionResult GetMessages(int chatId)
        {
            var data = new List<object>();
            data.Add(chatId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 1, Data = data }));
            return Ok();
        }

        [HttpGet("message/{messageId}", Name = "GetMessage")]
        public IActionResult GetMessage(int messageId)
        {
            var data = new List<object>();
            data.Add(messageId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 2, Data = data }));
            return Ok();
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public IActionResult GetChat(int chatId)
        {
            var data = new List<object>();
            data.Add(chatId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 3, Data = data }));
            return Ok();
        }

        [HttpDelete("chat/{chatId}")]
        public IActionResult DeleteChat(int chatId)
        {
            var data = new List<object>();
            data.Add(chatId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 4, Data = data }));
            return Ok();
        }

        [HttpDelete("message/{messageId}")]
        public IActionResult DeleteMessage(int messageId)
        {
            var data = new List<object>();
            data.Add(messageId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 5, Data = data }));
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] ChatForm chatdto)
        {
            var data = new List<object>();
            data.Add(chatdto);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 6, Data = data }));
            return Ok();
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public IActionResult AddUser(int chatId, int userId)
        {
            var data = new List<object>();
            data.Add(chatId);
            data.Add(userId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 7, Data = data }));
            return Ok();
        }

        [HttpPut("addmessage/{chatId}")]
        public IActionResult AddMessage([FromForm] MessageForm messagedto, int chatId)
        {
            var data = new List<object>();
            data.Add(messagedto);
            data.Add(chatId);
            _workerService.Enqueue(JsonSerializer.Serialize(new WorkersDTO() { RequestType = 8, Data = data }));
            return Ok();
        }
    }       
}
