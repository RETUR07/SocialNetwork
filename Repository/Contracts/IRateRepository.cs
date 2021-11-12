using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetworks.Repository.Contracts
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<IEnumerable<Rate>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        Task<Rate> GetPostRateAsync(int userId, int postId, bool trackChanges);
    }
}
