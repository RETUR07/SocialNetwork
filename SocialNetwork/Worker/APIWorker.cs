using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Controllers;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Worker
{
    public class APIWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "APIWorkerQueue";

        public APIWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var rabbitHostName = "rabbitmq";

            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitHostName,
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(message);

                int i = message.IndexOf(" ");
                int messageLogId = int.Parse(message[..i].Trim());
                JObject dto = JObject.Parse(message[i..].Trim());
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IChatWorkerService _chatWorkerService =
                        scope.ServiceProvider.GetRequiredService<IChatWorkerService>();
                    await _chatWorkerService.ProcessMessage(dto, messageLogId);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
        }
    }
}
