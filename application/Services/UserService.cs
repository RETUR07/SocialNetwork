using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

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

            if (user.Subscribers.Contains(friend))
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

            if (friend.Subscribers.Contains(user))
            {
                friend.Subscribers.Remove(user);
            }
            if (user.Friends.Contains(friend) || friend.Friends.Contains(user))
            {
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);

                user.Subscribers.Add(friend);
            }
            await _repository.SaveAsync();
        }

        public async Task<User> CreateUserAsync(UserRegistrationForm userdto)
        {
            if (userdto == null)
            {
                return null;
            }

            if (_repository.User.FindByCondition(u => u.Username == userdto.Username, false).Count() != 0) return null;
            var user = _mapper.Map<User>(userdto);
            user.PasswordHash = BCryptNet.HashPassword(userdto.Password);
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

        public async Task<List<UserForResponseDTO>> GetUsersAsync()
        {
            var users = await _repository.User.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<List<UserForResponseDTO>>(users);
            return usersdto;
        }

        public async Task UpdateUserAsync(int userId, UserForm userdto)
        {
            if (userdto == null) throw new InvalidDataException("user dto is null");

            var user = await _repository.User.GetUserAsync(userId, true);

            if (user == null) throw new InvalidDataException("no such user");

            _mapper.Map(userdto, user);
            await _repository.SaveAsync();
        }

        public async Task AdminUpdateUserAsync(int userId, AdminUserForm userdto)
        {
            if (userdto == null) throw new InvalidDataException("user dto is null");

            var user = await _repository.User.GetUserAsync(userId, true);

            if (user == null) throw new InvalidDataException("no such user");

            user.Role = userdto.Role;
            await _repository.SaveAsync();
        }
    }
}
