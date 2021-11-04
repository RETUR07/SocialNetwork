using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(int userId, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
