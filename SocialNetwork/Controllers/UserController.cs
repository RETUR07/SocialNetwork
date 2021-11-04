using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Contracts;
using SocialNetwork.Models;
using SocialNetwork.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.user.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<IEnumerable<UserForResponseDTO>>(users);
            return Ok(usersdto);
        }

        [HttpGet("{userId}", Name = "GetUser")]    
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _repository.user.GetUserAsync(userId, false);
            if (user == null)
            {
                return NotFound();
            }
            var userdto = _mapper.Map<UserForResponseDTO>(user);
            return Ok(userdto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForUpdateOrCreationDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest("user is null");
            }
            var user = _mapper.Map<User>(userdto);
            _repository.user.CreateUser(user);
            await _repository.SaveAsync();
            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserForUpdateOrCreationDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest("userdto is null");
            }

            var user = await _repository.user.GetUserAsync(userId, true);

            if (user == null)
            {
                return BadRequest("user is null");
            }

            _mapper.Map(userdto, user);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _repository.user.GetUserAsync(userId, true);
            _repository.user.DeleteUser(user);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
