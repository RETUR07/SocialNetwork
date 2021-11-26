using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SocialNetwork.Application.Contracts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Worker
{
    public class APIWorker : BackgroundService
    {
        private readonly IWorkerService _workerService;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "hello";

        public APIWorker(IWorkerService workerService)
        {
            _workerService = workerService;
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
            _channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                int logId = int.Parse(message.Split()[0]);
                _workerService.UpdateLog(logId, "Recieved from worker");
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
