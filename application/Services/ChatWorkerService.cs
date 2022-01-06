using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetworks.Repository.Contracts;

using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class ChatWorkerService : IChatWorkerService
    {
        private readonly IChatService _chatService;
        private readonly IWorkerService _workerService;
        private readonly ILogRepositoryManager _logRepositoryManager;

        public ChatWorkerService(IChatService chatService, IWorkerService workerService, ILogRepositoryManager logRepositoryManager)
        {
            _chatService = chatService;
            _workerService = workerService;
            _logRepositoryManager = logRepositoryManager;
        }

        public async Task ProcessMessage(JObject workerDTO, int messageLogId)
        {
            var messageLog = _logRepositoryManager.MessageLog.GetMessageLog(messageLogId, true);
            messageLog.MessageStatus = "Recieved from worker";
            await _logRepositoryManager.SaveAsync();

            switch ((int)workerDTO["RequestType"])
            {
                case 0:
                    var chatsdto = await _chatService.GetChats((int)workerDTO["UserId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + JsonConvert.SerializeObject(chatsdto));
                    break;
                case 1:
                    var messagesdto = await _chatService.GetMessages((int)workerDTO["UserId"], (int)workerDTO["ChatId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + JsonConvert.SerializeObject(messagesdto));
                    break;
                case 2:
                    var messagedto = await _chatService.GetMessage((int)workerDTO["MessageId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + JsonConvert.SerializeObject(messagedto));
                    break;
                case 3:
                    var chatdto = await _chatService.GetChat((int)workerDTO["UserId"], (int)workerDTO["ChatId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + JsonConvert.SerializeObject(chatdto));
                    break;
                case 4:
                    await _chatService.DeleteChat((int)workerDTO["ChatId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + "completed");
                    break;
                case 5:
                    await _chatService.DeleteMessage((int)workerDTO["UserId"], (int)workerDTO["UserId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + "completed");
                    break;
                case 6:
                    await _chatService.CreateChat(JsonConvert.DeserializeObject<ChatForm>(workerDTO["Chatdto"].ToString()));
                    await _workerService.EnqueueAsync(messageLogId + " " + "completed");
                    break;
                case 7:
                    await _chatService.AddUser((int)workerDTO["ChatId"], (int)workerDTO["UserId"], (int)workerDTO["UserId"]);
                    await _workerService.EnqueueAsync(messageLogId + " " + "completed");
                    break;
                case 8:
                    await _chatService.AddMessage((int)workerDTO["UserId"], JsonConvert.DeserializeObject<MessageForm>(workerDTO["Messagedto"].ToString()));
                    await _workerService.EnqueueAsync(messageLogId + " " + "completed");
                    break;
            }
        }
    }
}
