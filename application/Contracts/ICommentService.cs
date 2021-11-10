using SocialNetwork.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface ICommentService
    {
        public Task<CommentForResponseDTO> GetCommentASync(int userId, int postId, bool trackChanges);
        public Task<IEnumerable<CommentForResponseDTO>> GetCommentsByPostIdAsync(int postId, bool trackChanges);
        public Task<CommentForResponseDTO> CreateCommentAsync(CommentForm commentForm);
        public Task UpdateCommentAsync(CommentForm commentForm);
    }
}
