using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IChatRepository : IRepositoryBase<Chat>
    {
        public Task<IEnumerable<Chat>> GetChatsAsync(User user, bool trackChanges);
        public Task<Chat> GetChatAsync(int chatId, bool trackChanges);
    }
}
