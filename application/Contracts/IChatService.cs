using Microsoft.AspNetCore.Http;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IChatService
    {
        public Task<List<ChatForResponseDTO>> GetChats(int userId);
        public Task<List<MessageForResponseDTO>> GetMessages(int userId, int chatId);
        public Task<ChatForResponseDTO> GetChat(int userId, int chatId);
        public Task DeleteChat(int chatId);
        public Task DeleteMessage(int userId, int messageId);
        public Task<MessageForResponseDTO> GetMessage(int messageId);
        public Task<Chat> CreateChat(int userId, ChatForm chatdto);
        public Task AddUser(int chatId, int userId, int adderId);
        public Task<MessageForResponseDTO> AddMessage(int UserId, MessageForm messagedto);
        public Task<MessageForResponseDTO> AddFilesToMessage(int UserId, int messageId, IEnumerable<IFormFile> formFiles);
    }
}
