using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectRepository.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(int userId, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
