using SocialNetwork.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IRateService
    {
        public Task<RateForResponseDTO> GetRateAsync(int userId, int postId, bool trackChanges);
        public Task<IEnumerable<RateForResponseDTO>> GetRatesByUserIdAsync(int userId, bool trackChanges);
        public Task<IEnumerable<RateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        public Task<RateForResponseDTO> CreateRateAsync(RateForm rate);
        public Task UpdateRateAsync(RateForm rate);
    }
}
