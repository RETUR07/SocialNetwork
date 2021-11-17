﻿using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IChatService
    {
        public Task<IEnumerable<ChatForResponseDTO>> GetChats(int userId);
        public Task<IEnumerable<MessageForResponseDTO>> GetMessages(int chatId);
        public Task<ChatForResponseDTO> GetChat(int chatId);
        public Task DeleteChat(int chatId);
        public Task DeleteMessage(int messageId);
        public Task<MessageForResponseDTO> GetMessage(int messageId);
        public Task<Chat> CreateChat(ChatForm chatdto);
        public Task<bool> AddUser(int chatId, int userId);
        public Task<bool> AddMessage(int chatId, MessageForm messagedto);
    }
}