using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync(User user, bool trackChanges)
            => await FindByCondition(ch => ch.Users.Contains(user), trackChanges)
            .Include(x => x.Users.Where(x => x.IsEnable))
            .ToListAsync();

        public async Task<Chat> GetChatAsync(int chatId, bool trackChanges)
            => await FindByCondition(ch => ch.Id == chatId, trackChanges)
            .Include(x => x.Users.Where(x => x.IsEnable))
            .Include(x => x.Messages.Where(x => x.IsEnable)).ThenInclude(x => x.User)
            .SingleOrDefaultAsync();
    }
}
