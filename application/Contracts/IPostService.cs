using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto, int userId);
        public Task DeletePost(int postId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<List<PostForResponseDTO>> GetPosts(int userId);
        public Task<List<PostForResponseDTO>> GetChildPosts(int postId);
    }
}
