using SocialNetwork.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IRateService
    {
        public Task<PostRateForResponseDTO> GetPostRateAsync(int userId, int postId, bool trackChanges);
        public Task<PostRateForResponseDTO> GetCommentRateAsync(int userId, int commentId, bool trackChanges);
        public Task<IEnumerable<PostRateForResponseDTO>> GetRatesByPostIdAsync(int postId, bool trackChanges);
        public Task<IEnumerable<CommentRateForResponseDTO>> GetRatesByCommentIdAsync(int commentId, bool trackChanges);
        public Task UpdateCommentRateAsync(RateForm rate);
        public Task UpdatePostRateAsync(RateForm rate);
    }
}
