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

        public async Task<Rate> GetPostRateAsync(int userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.User.Id == userId && r.Post.Id == postId, trackChanges)
            .Include(x => x.Post).Include(x => x.User).SingleOrDefaultAsync();

        public async Task<List<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.Post.Id == postId, trackChanges)
            .Include(x => x.User).ToListAsync();
    }
}
