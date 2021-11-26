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
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [Route("{url}")]
        public EmptyResult CatchMessage()
        {          
            int messageId = _workerService.LogToDatabase(JsonSerializer.Serialize(HttpContext.Request));
            _workerService.Enqueue(HttpContext.Request.Path + messageId.ToString(), "sent to api", messageId);
            return new EmptyResult(); 
        }
    }
}
