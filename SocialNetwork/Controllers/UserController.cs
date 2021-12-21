using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Base
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersdto = await _userService.GetUsersAsync();
            if (usersdto == null) return NotFound();
            return Ok(usersdto);
        }

        [HttpGet("info", Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationForm userdto)
        {
            var user = await _userService.CreateUserAsync(userdto);
            if (user == null)
            {
                return BadRequest("User is null");
            }
            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UserForm userdto)
        {
            try
            {
                await _userService.UpdateUserAsync(UserId, userdto);
            }
            catch(InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("admin/{userId}")]
        public async Task<IActionResult> AdminUpdateUser(int userId, [FromBody] AdminUserForm userdto)
        {
            try
            {
                await _userService.AdminUpdateUserAsync(userId, userdto);
            }
            catch (InvalidDataException exc)
            {
                return BadRequest(exc.Message);
            }
            return NoContent();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteUser()
        {
            await _userService.DeleteUserAsync(UserId);
            return NoContent();
        }

        [HttpPut("addfriend/{friendId}")]
        public async Task<IActionResult> AddFriend(int friendId)
        {
            await _userService.AddFriendAsync(UserId, friendId);
            return NoContent();
        }

        [HttpPut("deletefriend/{friendId}")]
        public async Task<IActionResult> DeleteFriend(int friendId)
        {
            await _userService.DeleteFriendAsync(UserId, friendId);
            return NoContent();
        }
    }
}
