using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetwork.Worker.Controllers
{
    [ApiController]
    [Route("")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [Route("{url}")]
        public IActionResult CatchMessage()
        {
            _workerService.Enqueue(JsonSerializer.Serialize(HttpContext));
            return Ok("hello"); 
        }
    }
}
