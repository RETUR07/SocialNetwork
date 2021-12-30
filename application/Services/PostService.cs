﻿using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using SocialNetworks.Repository.RequestFeatures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;
        public PostService(IRepositoryManager repository, IMapper mapper, IBlobService blobService)
        {
            _repository = repository;
            _mapper = mapper;
            _blobService = blobService;
        }

        public async Task<Post> CreatePost(PostForm postdto, int userId)
        {
            if (postdto == null)
            {
                return null;
            }
            var post = _mapper.Map<Post>(postdto);
            var user = await _repository.User.GetUserAsync(userId, true);
            var parentpost = await _repository.Post.GetPostAsync(postdto.ParentPostId, true);
            if (user == null || parentpost == null) return null;
            post.Author = user;
            post.ParentPost = parentpost;
            await _blobService.SaveBlobsAsync(postdto.Content, userId.ToString());
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

        public async Task<PostForResponseDTO> GetPost(int postId)
        {
            var post = await _repository.Post.GetPostAsync(postId, false);
            if (post == null)
            {
                return null;
            }
            var postdto = _mapper.Map<PostForResponseDTO>(post);
            postdto.Content = await _blobService.GetBLobsAsync(post.BlobIds.Select(x => x.Id), false);
            return postdto;
        }

        public async Task<PagedList<PostForResponseDTO>> GetChildPosts(int postId, Parameters parameters)
        {
            var posts = await _repository.Post.GetChildrenPostsByPostIdPagedAsync(postId, parameters, false);
            var postsdto = _mapper.Map<PagedList<Post>, PagedList <PostForResponseDTO>>(posts);
            for (int i = 0; i < posts.Count(); i++)
            {
                postsdto[i].Content = await _blobService.GetBLobsAsync(posts[i].BlobIds.Select(x => x.Id), false);
            }
            return postsdto;
        }

        public async Task<PagedList<PostForResponseDTO>> GetPosts(int userId, Parameters parameters)
        {
            var posts = await _repository.Post.GetAllPostsPagedAsync(userId, parameters, false);
            var postsdto = _mapper.Map<PagedList<Post>, PagedList<PostForResponseDTO>>(posts);
            for (int i = 0; i < posts.Count(); i++)
            {
                postsdto[i].Content = await _blobService.GetBLobsAsync(posts[i].BlobIds.Select(x => x.Id), false);
            }
            return postsdto;
        }
    }
}
