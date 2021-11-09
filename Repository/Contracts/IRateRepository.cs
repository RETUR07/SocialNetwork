using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepository.Contracts
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<Rate> GetRateAsync(int userId, int postId, bool trackChanges);
        Task<IEnumerable<Rate>> GetRatesByUserIdAsync(int userId, bool trackChanges);
        Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges);
    }
}
