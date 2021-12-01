using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
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
        private readonly IWorkerService _workerService;

        public ChatWorkerService(IChatService chatService, IWorkerService workerService)
        {
            _chatService = chatService;
            _workerService = workerService;
        }
        public async Task ProcessMessage(JObject workerDTO)
        {
            switch ((int)workerDTO["RequestType"])
            {
                case 0:
                    await _chatService.GetChats((int)workerDTO["UserId"]);
                    _workerService.Enqueue("completed");
                    break;
                case 1:
                    var messagesdto = await _chatService.GetMessages((int)workerDTO["ChatId"]);
                    _workerService.Enqueue(JsonConvert.SerializeObject(messagesdto));
                    break;
                case 2:
                    var messagedto = await _chatService.GetMessage((int)workerDTO["MessageId"]);
                    _workerService.Enqueue(JsonConvert.SerializeObject(messagedto));
                    break;
                case 3:
                    var chatdto = await _chatService.GetChat((int)workerDTO["ChatId"]);
                    _workerService.Enqueue(JsonConvert.SerializeObject(chatdto));
                    break;
                case 4:
                    await _chatService.DeleteChat((int)workerDTO["ChatId"]);
                    _workerService.Enqueue("completed");
                    break;
                case 5:
                    await _chatService.DeleteMessage((int)workerDTO["UserId"]);
                    _workerService.Enqueue("completed");
                    break;
                case 6:
                    await _chatService.CreateChat(JsonConvert.DeserializeObject<ChatForm>(workerDTO["Chatdto"].ToString()));
                    _workerService.Enqueue("completed");
                    break;
                case 7:
                    await _chatService.AddUser((int)workerDTO["ChatId"], (int)workerDTO["UserId"]);
                    _workerService.Enqueue("completed");
                    break;
                case 8:
                    await _chatService.AddMessage((int)workerDTO["ChatId"], JsonConvert.DeserializeObject<MessageForm>(workerDTO["Messagedto"].ToString()));
                    _workerService.Enqueue("completed");
                    break;
            }
        }
    }
}
