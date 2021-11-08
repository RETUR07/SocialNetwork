using Application.Contracts;
using Application.DTO;
using AutoMapper;
using Entities.Models;
using ProjectRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public PostService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Post> CreatePost(PostForm postdto)
        {
            if (postdto == null)
            {
                return null;
            }
            var post = _mapper.Map<Post>(postdto);
            _repository.post.CreatePost(post);
            await _repository.SaveAsync();
            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _repository.post.GetPostAsync(postId, true);
            _repository.post.DeletePost(post);
            await _repository.SaveAsync();
        }

        public async Task<PostForResponseDTO> GetPost(int postId)
        {
            var post = await _repository.user.GetUserAsync(postId, false);
            if (post == null)
            {
                return null;
            }
            var postdto = _mapper.Map<PostForResponseDTO>(post);
            return postdto;
        }

        public async Task<IEnumerable<PostForResponseDTO>> GetPosts()
        {
            var posts = await _repository.post.GetAllPostsAsync(false);
            var postsdto = _mapper.Map<IEnumerable<PostForResponseDTO>>(posts);
            return postsdto;
        }
    }
}
