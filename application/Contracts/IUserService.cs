using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponseDTO>> GetUsersAsync();
        public Task<UserForResponseDTO> GetUserAsync(int userId);
        public Task<User> CreateUserAsync(UserForm userdto);
        public Task<bool> UpdateUserAsync(int userId, UserForm userdto);
        public Task DeleteUserAsync(int userId);
        public Task AddFriendAsync(int userId, int friendId);
        public Task DeleteFriendAsync(int userId, int friendId);
    }
}
