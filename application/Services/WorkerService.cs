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
        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;
        private readonly string _queueName;

        public WorkerService(string queueName)
        {
            _queueName = queueName;
            _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        public void Enqueue(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                routingKey: _queueName,
                                basicProperties: null,
                                body: body);
        }

        public void CloseConnection()
        {
            _conn?.Close();
        }
    }
}
