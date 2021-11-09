﻿using Application.DTO;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponseDTO>> GetUsersAsync();
        public Task<UserForResponseDTO> GetUserAsync(int userId);
        public Task<User> CreateUserAsync(UserForm userdto);
        public Task<bool> UpdateUserAsync(int userId, UserForm userdto);
        public Task DeleteUserAsync(int userId);
    }
}
