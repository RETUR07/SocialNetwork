using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto);
        public Task DeletePost(int postId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<IEnumerable<PostForResponseDTO>> GetPosts(int userId);
        public Task<IEnumerable<PostForResponseDTO>> GetChildPosts(int postId);
    }
}
