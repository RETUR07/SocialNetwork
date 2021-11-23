using SocialNetwork.Application.DTO;
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
        public Task<List<ChatForResponseDTO>> GetChats(int userId);
        public Task<List<MessageForResponseDTO>> GetMessages(int chatId);
        public Task<ChatForResponseDTO> GetChat(int chatId);
        public Task DeleteChat(int chatId);
        public Task DeleteMessage(int messageId);
        public Task<MessageForResponseDTO> GetMessage(int messageId);
        public Task<Chat> CreateChat(ChatForm chatdto);
        public Task AddUser(int chatId, int userId);
        public Task AddMessage(int chatId, MessageForm messagedto);
    }
}
