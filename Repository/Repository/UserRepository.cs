using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public new void Delete(User entity)
        {
            entity.Friends.Clear();
            entity.MakedFriend.Clear();
            entity.Subscribed.Clear();
            entity.Subscribers.Clear();
            base.Delete(entity);
        }

        public async Task<List<User>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(u => u.LastName).ToListAsync();

        public async Task<User> GetUserAsync(int userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges)
            .Include(x => x.Friends).OrderBy(x => x.Id)
            .Include(x => x.MakedFriend).OrderBy(x => x.Id)
            .Include(x => x.Subscribers).OrderBy(x => x.Id)
            .Include(x => x.Subscribed).OrderBy(x => x.Id)
            .AsSplitQuery()        
            .SingleOrDefaultAsync();
    }
}
