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

        public async Task<Rate> GetRateAsync(int userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.UserId == userId && r.PostId == postId, trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.PostId == postId, trackChanges).ToListAsync();

        public async Task<IEnumerable<Rate>> GetRatesByUserIdAsync(int userId, bool trackChanges)=>
            await FindByCondition(r => r.UserId == userId, trackChanges).ToListAsync();
    }
}
