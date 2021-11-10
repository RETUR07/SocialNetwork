using SocialNetwork.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        public Task<Comment> GetCommentAsync(int userId, int postId, bool trackChanges);
        public Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId, bool trackChanges);
    }
}
