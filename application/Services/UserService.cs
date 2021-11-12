﻿using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddFriendAsync(int userId, int friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;
            if (user.Friends.Contains(friend) || friend.Friends.Contains(user)) return;

            if(user.Subscribers.Contains(friend))
            {
                user.Friends.Add(friend);
                user.Subscribers.Remove(friend);
            }
            else
            {
                friend.Subscribers.Add(user);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteFriendAsync(int userId, int friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;

            if(friend.Subscribers.Contains(user))
            {
                friend.Subscribers.Remove(user);
            }
            if(user.Friends.Contains(friend) || friend.Friends.Contains(user))
            {
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);

                user.Subscribers.Add(friend);
            }
            await _repository.SaveAsync();
        }

        public async Task<User> CreateUserAsync(UserForm userdto)
        {
            if (userdto == null)
            {
                return null;
            }
            var user = _mapper.Map<User>(userdto);
            _repository.User.Create(user);
            await _repository.SaveAsync();
            return user;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            _repository.User.Delete(user);
            await _repository.SaveAsync();
        }

        public async Task<UserForResponseDTO> GetUserAsync(int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, false);
            if (user == null)
            {
                return null;
            }
            var userdto = _mapper.Map<UserForResponseDTO>(user);
            return userdto;
        }

        public async Task<IEnumerable<UserForResponseDTO>> GetUsersAsync()
        {
            var users = await _repository.User.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<IEnumerable<UserForResponseDTO>>(users);
            return usersdto;
        }

        public async Task<bool> UpdateUserAsync(int userId, UserForm userdto)
        {
            if (userdto == null)
            {
                return false;
            }

            var user = await _repository.User.GetUserAsync(userId, true);

            if (user == null)
            {
                return false;
            }

            _mapper.Map(userdto, user);
            await _repository.SaveAsync();
            return true;
        }
    }
}
