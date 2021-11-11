using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Rate> GetCommentRateAsync(int userId, int commentId, bool trackChanges) =>
           await FindByCondition(r => r.User.Id == userId && r.Comment.Id == commentId, trackChanges)
            .Include(x => x.Comment).Include(x => x.User).SingleOrDefaultAsync();

        public async Task<Rate> GetPostRateAsync(int userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.User.Id == userId && r.Post.Id == postId, trackChanges)
            .Include(x => x.Post).Include(x => x.User).SingleOrDefaultAsync();

        public async Task<IEnumerable<Rate>> GetRatesByCommentIdAsync(int commentId, bool trackChanges) =>
           await FindByCondition(r => r.Comment.Id == commentId, trackChanges)
            .Include(x => x.User).ToListAsync();

        public async Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.Post.Id == postId, trackChanges)
            .Include(x => x.User).ToListAsync();
    }
}
