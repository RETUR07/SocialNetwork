using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
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
            var user = await _repository.User.GetUserAsync(postdto.UserId, true);
            var parentpost = await _repository.Post.GetPostAsync(postdto.ParentPostId, true);
            if (user == null || parentpost == null) return null;
            post.Author = user;
            post.ParentPost = parentpost;
            _repository.Post.Create(post);
            await _repository.SaveAsync();
            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _repository.Post.GetPostAsync(postId, true);
            _repository.Post.Delete(post);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<PostForResponseDTO>> GetChildPosts(int postId)
        {
            var posts = await _repository.Post.GetChildrenPostsByPostIdAsync(postId, false);
            var postsdto = _mapper.Map<IEnumerable<PostForResponseDTO>>(posts);
            return postsdto;
        }

        public async Task<PostForResponseDTO> GetPost(int postId)
        {
            var post = await _repository.Post.GetPostAsync(postId, false);
            if (post == null)
            {
                return null;
            }
            var postdto = _mapper.Map<PostForResponseDTO>(post);
            return postdto;
        }

        public async Task<IEnumerable<PostForResponseDTO>> GetPosts(int userId)
        {
            var posts = await _repository.Post.GetAllPostsAsync(userId, false);
            var postsdto = _mapper.Map<IEnumerable<PostForResponseDTO>>(posts);
            return postsdto;
        }
    }
}
