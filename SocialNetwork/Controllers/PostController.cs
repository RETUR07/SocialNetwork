using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var postsdto = await _postService.GetPosts();
            return Ok(postsdto);
        }

        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<IActionResult> GetUser(int postId)
        {
            var post = await _postService.GetPost(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostForm postdto)
        {
            var post = await _postService.CreatePost(postdto);
            if (post == null)
            {
                return BadRequest("Post is null");
            }
            return CreatedAtRoute("GetPost", new { postId = post.Id }, post);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteUser(int postId)
        {
            await _postService.DeletePost(postId);
            return NoContent();
        }
    }
}
