using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IWorkerService
    {
        void UpdateLog(int logId, string status);
        int LogToDatabase(string message);
        void Enqueue(string message, string status, int messageId);
    }
}
