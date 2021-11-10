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

        [HttpGet("{userId}/{postId}", Name = "GetRate")]
        public async Task<IActionResult> GetRate(int userId, int postId)
        {
            var rate = await _rateService.GetRateAsync(userId, postId, false);
            if (rate == null)
                return NotFound();
            return Ok(rate);
        }

        [HttpGet("userrates/{userId}")]
        public async Task<IActionResult> GetRatesOfUser(int userId)
        {
            var rates = await _rateService.GetRatesByUserIdAsync(userId, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpGet("postrates/{postId}")]
        public async Task<IActionResult> GetRatesOfPost(int postId)
        {
            var rates = await _rateService.GetRatesByPostIdAsync(postId, false);
            if (rates == null)
                return NotFound();
            return Ok(rates);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRate(RateForm rateForm)
        {
            var ratedto = await _rateService.CreateRateAsync(rateForm);
            if (ratedto == null)
            {
                return BadRequest("rate is null");
            }
            return CreatedAtRoute("GetRate", new { userId = ratedto.UserId, postId = ratedto.PostId }, ratedto);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRate([FromBody]RateForm rateForm)
        {
            if (rateForm == null)
            {
                return BadRequest("RateForm is null");
            }
            await _rateService.UpdateRateAsync(rateForm);        
            return NoContent();
        }
    }
}
