using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{userId}/{postId}", Name = "GetComment")]
        public async Task<IActionResult> GetComment(int userId, int postId)
        {
            var comment = await _commentService.GetCommentASync(userId, postId, false);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }

        [HttpGet("postcomments/{postId}")]
        public async Task<IActionResult> GetCommentsOfPost(int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId, false);
            if (comments == null)
                return NotFound();
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentForm commentForm)
        {
            var commentdto = await _commentService.CreateCommentAsync(commentForm);
            if (commentdto == null)
            {
                return BadRequest("comment is null");
            }
            return CreatedAtRoute("GetComment", new { userId = commentdto.User.Id, postId = commentdto.Post.Id }, commentdto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRate([FromBody] CommentForm commentForm)
        {
            if (commentForm == null)
            {
                return BadRequest("CommentForm is null");
            }
            await _commentService.UpdateCommentAsync(commentForm);
            return NoContent();
        }
    }
}
