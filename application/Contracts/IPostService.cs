using Application.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto);
        public Task DeletePost(int postId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<IEnumerable<PostForResponseDTO>> GetPosts();
    }
}
