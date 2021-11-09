using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<Rate> GetRateAsync(int userId, int postId, bool trackChanges);
        Task<IEnumerable<Rate>> GetRatesByUserIdAsync(int userId, bool trackChanges);
        Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges);
    }
}
