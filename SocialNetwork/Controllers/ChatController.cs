﻿using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SocialNetwork.Application.Exceptions;
using System.Text.Json;
using SocialNetwork.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Base
    {
        private readonly IChatService _chatService;
        IHubContext<ChatHub> _hubContext;

        public ChatController(IChatService chatService, IHubContext<ChatHub> hubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
        }

        [HttpGet("chats/")]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatService.GetChats(UserId);
            if (chats == null)
            {
                return BadRequest();
            }
            return Ok(chats);
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _chatService.GetMessages(UserId, chatId);
            if (messages == null) return NotFound();
            return Ok(messages);
        }

        [HttpGet("{chatId}", Name = "GetChat")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            var chat = await _chatService.GetChat(UserId, chatId);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("chat/{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            await _chatService.DeleteChat(chatId);
            return NoContent();
        }

        [HttpDelete("message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            await _chatService.DeleteMessage(UserId, messageId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatForm chatdto)
        {
            var chat = await _chatService.CreateChat(UserId, chatdto);
            if (chat == null)
            {
                return BadRequest("Chat is null");
            }
            return CreatedAtRoute("GetChat", new { chatId = chat.Id }, await _chatService.GetChat(UserId, chat.Id));
        }

        [HttpPut("{chatId}/adduser/{userId}")]
        public async Task<IActionResult> AddUser(int chatId, int userId)
        {
            try
            {
                await _chatService.AddUser(chatId, userId, UserId);
            }
            catch(InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }

        [HttpPut("addmessage")]
        public async Task<IActionResult> AddMessage([FromForm]MessageForm messagedto)
        {
            try
            {
                var message = await _chatService.AddMessage(UserId, messagedto);
                await _hubContext.Clients.All.SendAsync("Send", message);
            }
            catch (InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }
    }
}
