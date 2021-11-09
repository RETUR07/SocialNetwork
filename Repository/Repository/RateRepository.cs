using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ProjectRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<Rate> GetRateAsync(int userId, int postId, bool trackChanges) =>
           await FindByCondition(r => r.userId == userId && r.postId == postId, trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges) =>
           await FindByCondition(r => r.postId == postId, trackChanges).ToListAsync();

        public async Task<IEnumerable<Rate>> GetRatesByUserIdAsync(int userId, bool trackChanges)=>
            await FindByCondition(r => r.userId == userId, trackChanges).ToListAsync();
    }
}
