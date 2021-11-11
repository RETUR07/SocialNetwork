using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("post/{userId}/{postId}")]
        public async Task<IActionResult> GetRate(int userId, int postId)
        {
            var rate = await _rateService.GetPostRateAsync(userId, postId, false);
            if (rate == null)
                return NotFound();
            return Ok(rate);
        }

        [HttpGet("comment/{userId}/{commentId}")]
        public async Task<IActionResult> GetComment(int userId, int commentId)
        {
            var rate = await _rateService.GetCommentRateAsync(userId, commentId, false);
            if (rate == null)
                return NotFound();
            return Ok(rate);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetRatesOfPost(int postId)
        {
            var rates = await _rateService.GetRatesByPostIdAsync(postId, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpGet("comment/{commentId}")]
        public async Task<IActionResult> GetRatesOfcomment(int commentId)
        {
            var rates = await _rateService.GetRatesByCommentIdAsync(commentId, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpPut("post")]
        public async Task<IActionResult> UpdatePostRate([FromBody]RateForm rateForm)
        {
            if (rateForm == null)
            {
                return BadRequest("RateForm is null");
            }
            await _rateService.UpdatePostRateAsync(rateForm);        
            return NoContent();
        }

        [HttpPut("comment")]
        public async Task<IActionResult> UpdateCommentRate([FromBody] RateForm rateForm)
        {
            if (rateForm == null)
            {
                return BadRequest("RateForm is null");
            }
            await _rateService.UpdateCommentRateAsync(rateForm);
            return NoContent();
        }
    }
}
