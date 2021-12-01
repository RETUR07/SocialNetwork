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
            if (entity.Friends != null)
                entity.Friends.Clear();
            if (entity.MakedFriend != null)
                entity.MakedFriend.Clear();
            if (entity.Subscribed != null)
                entity.Subscribed.Clear();
            if (entity.Subscribers != null)
                entity.Subscribers.Clear();
            base.Delete(entity);
        }

        public async Task<List<User>> GetAllUsersAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(u => u.LastName).ToListAsync();

        public async Task<User> GetUserAsync(int userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges)
            .Include(x => x.Friends)
            .Include(x => x.MakedFriend)
            .Include(x => x.Subscribers)
            .Include(x => x.Subscribed)
            .AsSplitQuery()
            .SingleOrDefaultAsync();
    }
}
