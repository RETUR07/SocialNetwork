using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Base
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

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetRatesOfPost(int postId)
        {
            var rates = await _rateService.GetRatesByPostIdAsync(postId, false);
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
            await _rateService.UpdatePostRateAsync(rateForm, UserId);        
            return NoContent();
        }
    }
}
