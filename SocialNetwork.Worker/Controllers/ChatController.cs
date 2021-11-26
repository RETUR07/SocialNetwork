using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Worker.Controllers
{
    internal class ChatController : ControllerBase
    {
        [HttpGet("chats/{userId}")]
        public RedirectToRouteResult GetChats(int userId)
        {
            var redirectRoute = "http://web:80/api/Chat/chats/" + userId;
            return RedirectToRoute(redirectRoute);             
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages()
        {
            return NoContent();

        }

        [HttpGet("message/{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage()
        {
            return NoContent();

        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public async Task<IActionResult> GetChat()
        {
            return NoContent();

        }

        [HttpDelete("chat/{chatId}")]
        public async Task<IActionResult> DeleteChat()
        {
            return NoContent();

        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage()
        {
            return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> CreateChat()
        {
            return NoContent();

        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public async Task<IActionResult> AddUser()
        {
            return NoContent();

        }

        [HttpPut("addmessage/{chatId}")]
        public async Task<IActionResult> AddMessage()
        {
            return NoContent();

        }
    }
}
