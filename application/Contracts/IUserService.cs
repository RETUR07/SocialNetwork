using Application.DTO;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponseDTO>> GetUsers();
        public Task<UserForResponseDTO> GetUser(int userId);
        public Task<User> CreateUser(UserForm userdto);
        public Task<bool> UpdateUser(int userId, UserForm userdto);
        public Task DeleteUser(int userId);
    }
}
