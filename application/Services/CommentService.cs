using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CommentService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommentForResponseDTO> CreateCommentAsync(CommentForm commentForm)
        {
            if (commentForm == null)
            {
                return null;
            }

            var user = await _repository.User.GetUserAsync(commentForm.User, true);
            var post = await _repository.Post.GetPostAsync(commentForm.Post, true);

            var comment = _mapper.Map<CommentForm, Comment>(commentForm);
            _mapper.Map(user, comment);
            _mapper.Map(post, comment);

            _repository.Comment.Create(comment);
            await _repository.SaveAsync();
            return _mapper.Map<Comment, CommentForResponseDTO>(comment);
        }

        public async Task<CommentForResponseDTO> GetCommentASync(int userId, int postId, bool trackChanges)
        {
            var comment = await _repository.Comment.GetCommentAsync(userId, postId, trackChanges);
            return _mapper.Map<Comment, CommentForResponseDTO>(comment);
        }

        public async Task<IEnumerable<CommentForResponseDTO>> GetCommentsByPostIdAsync(int postId, bool trackChanges)
        {
            var comments = await _repository.Comment.GetCommentsByPostIdAsync(postId, trackChanges);
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentForResponseDTO>>(comments);
        }

        public async Task UpdateCommentAsync(CommentForm commentForm)
        {
            if (commentForm == null) return;
            var comment = await _repository.Comment.GetCommentAsync(commentForm.User, commentForm.Post, true);
            if (comment == null)
            {
                var user = await _repository.User.GetUserAsync(commentForm.User, true);
                var post = await _repository.Post.GetPostAsync(commentForm.Post, true);

                comment = _mapper.Map<CommentForm, Comment>(commentForm);
                _mapper.Map(user, comment);
                _mapper.Map(post, comment);
                _repository.Comment.Create(comment);
            }
            else
            {
                if (commentForm.Text == "" || commentForm.Text == null)
                    _repository.Comment.Delete(comment);
                else
                    _mapper.Map(commentForm, comment);
            }
            await _repository.SaveAsync();
            return;
        }
    }
}
