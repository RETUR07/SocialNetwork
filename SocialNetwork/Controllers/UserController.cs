using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersdto = await _userService.GetUsersAsync();
            if (usersdto == null) return NotFound();
            return Ok(usersdto);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
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
            return CreatedAtRoute("GetUser", new { userId = user.Id }, user.Id);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserForm userdto)
        {
            var success = await _userService.UpdateUserAsync(userId, userdto);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }

        [HttpPut("addfriend/{userId}/{friendId}")]
        public async Task<IActionResult> AddFriend(int userId, int friendId)
        {
            await _userService.AddFriendAsync(userId, friendId);
            return NoContent();
        }

        [HttpPut("deletefriend/{userId}/{friendId}")]
        public async Task<IActionResult> DeleteFriend(int userId, int friendId)
        {
            await _userService.DeleteFriendAsync(userId, friendId);
            return NoContent();
        }
    }
}
