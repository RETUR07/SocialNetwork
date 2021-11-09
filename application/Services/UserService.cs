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
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    
        public async Task<User> CreateUserAsync(UserForm userdto)
        {
            if (userdto == null)
            {
                return null;
            }
            var user = _mapper.Map<User>(userdto);
            _repository.user.Create(user);
            await _repository.SaveAsync();
            return user;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _repository.user.GetUserAsync(userId, true);
            _repository.user.Delete(user);
            await _repository.SaveAsync();
        }

        public async Task<UserForResponseDTO> GetUserAsync(int userId)
        {
            var user = await _repository.user.GetUserAsync(userId, false);
            if (user == null)
            {
                return null;
            }
            var userdto = _mapper.Map<UserForResponseDTO>(user);
            return userdto;
        }

        public async Task<IEnumerable<UserForResponseDTO>> GetUsersAsync()
        {
            var users = await _repository.user.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<IEnumerable<UserForResponseDTO>>(users);
            return usersdto;
        }

        public async Task<bool> UpdateUserAsync(int userId, UserForm userdto)
        {
            if (userdto == null)
            {
                return false;
            }

            var user = await _repository.user.GetUserAsync(userId, true);

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
