using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpGet("{userId}/{postId}", Name = "GetRate")]
        public async Task<IActionResult> GetRate(int userId, int postId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{userId}/{postId}")]
        public async Task<IActionResult> CreateRate(int userId, int postId)
        {
            throw new NotImplementedException();
        }


        [HttpPut("{userId}/{postId}")]
        public async Task<IActionResult> UpdateRate(int userId, int postId)
        {
            throw new NotImplementedException();
        }
    }
}
