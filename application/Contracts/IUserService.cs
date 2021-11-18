using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.SecurityModels;
using SocialNetwork.Security.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponseDTO>> GetUsersAsync();
        public Task<UserForResponseDTO> GetUserAsync(int userId);
        public Task<User> CreateUserAsync(UserRegistrationForm userdto);
        public Task<bool> UpdateUserAsync(int userId, UserForm userdto);
        public Task DeleteUserAsync(int userId);
        public Task AddFriendAsync(int userId, int friendId);
        public Task DeleteFriendAsync(int userId, int friendId);
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        public AuthenticateResponse RefreshToken(string token, string ipAddress);
        public void RevokeToken(string token, string ipAddress);
        public Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(int id);
    }
}
