using Application.DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponseDTO>> GetUsers();
        public Task<UserForResponseDTO> GetUser(int userId);
        public Task<User> CreateUser(UserForUpdateOrCreationDTO userdto);
        public Task<bool> UpdateUser(int userId, UserForUpdateOrCreationDTO userdto);
        public Task DeleteUser(int userId);
    }
}
