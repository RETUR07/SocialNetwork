using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class ChatWorkerService : IChatWorkerService
    {
        private readonly IChatService _chatService;
        public ChatWorkerService(IChatService chatService )
        {
            _chatService = chatService;
        }
        public async Task ProcessMessage(WorkersDTO workerDTO)
        {
            

            switch (workerDTO.RequestType)
            {
                case 0:
                    await _chatService.GetChats((int)workerDTO.Data[0]);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
            }
        }
    }
}
