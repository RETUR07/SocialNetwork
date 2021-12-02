using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IChatWorkerService
    {
        Task ProcessMessage(JObject workerDTO, int messageLogId);
    }
}
