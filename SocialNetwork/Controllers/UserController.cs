using Application.Contracts;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
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
            var usersdto = await _userService.GetUsers();
            return Ok(usersdto);
        }

        [HttpGet("{userId}", Name = "GetUser")]    
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForm userdto)
        {
            var user = await _userService.CreateUser(userdto);
            if(user == null)
            {
                return BadRequest("User is null");
            }
            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserForm userdto)
        {
            var success = await _userService.UpdateUser(userId, userdto);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
