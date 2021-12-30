﻿using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IPostService
    {
        public Task<Post> CreatePost(PostForm postdto, int userId);
        public Task DeletePost(int postId);
        public Task<PostForResponseDTO> GetPost(int postId);
        public Task<PagedList<PostForResponseDTO>> GetPosts(int userId, Parameters parameters);
        public Task<PagedList<PostForResponseDTO>> GetChildPosts(int postId, Parameters parameters);
    }
}
