using Microsoft.EntityFrameworkCore;
using Repository;
using SocialNetwork.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public async Task<List<User>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(u => u.LastName).ToListAsync();

        public async Task<User> GetUserAsync(int userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges).SingleOrDefaultAsync();
    }
}
