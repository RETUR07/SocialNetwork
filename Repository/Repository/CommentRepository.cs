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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Comment> GetCommentAsync(int userId, int postId, bool trackChanges) =>
             await FindByCondition(r => r.User.Id == userId && r.Post.Id == postId, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId, bool trackChanges) =>
             await FindByCondition(r => r.Post.Id == postId, trackChanges)
            .Include(x => x.User).ToListAsync();
    }
}
