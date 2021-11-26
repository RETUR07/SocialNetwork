using RabbitMQ.Client;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly ILogRepositoryManager _logRepositoryManager;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        public WorkerService(ILogRepositoryManager logRepositoryManager)
        {
            _logRepositoryManager = logRepositoryManager;

            _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        public void Enqueue(string message, string status, int messageId)
        {
            var msg = _logRepositoryManager.MessageLog.GetMessageLog(messageId, false);
            if (message == null)
            {
                throw new Exception("invalid messageId");
            }
            var body = Encoding.UTF8.GetBytes(messageId.ToString());
            _channel.BasicPublish(exchange: "",
                                routingKey: "hello",
                                basicProperties: null,
                                body: body);
            msg.MessageStatus = status;
            _logRepositoryManager.MessageLog.Update(msg);
            _logRepositoryManager.SaveAsync();
        }

        public int LogToDatabase(string message)
        {
            var messageLog = new MessageLog() { Message = message, MessageStatus = "recieved from user" };
            _logRepositoryManager.MessageLog.Create(messageLog);
            _logRepositoryManager.SaveAsync();
            return messageLog.Id;
        }

        public void UpdateLog(int logId, string status)
        {
            var msg = _logRepositoryManager.MessageLog.GetMessageLog(logId, false);
            msg.MessageStatus = status;
            _logRepositoryManager.SaveAsync();
        }

        public void CloseConnection()
        {
            _conn?.Close();
        }
    }
}
