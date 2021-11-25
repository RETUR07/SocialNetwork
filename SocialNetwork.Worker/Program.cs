using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Worker
{
    class Program
    {
        public static async Task GetMessage()
        {


            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    await client.GetAsync("http://web:80/api/Chat/chats/1");
                }
            }
        }

        static void Main(string[] args)
        {
            GetMessage().Wait();
            ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received from Rabbit: {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                                    autoAck: true,
                                    consumer: consumer);
            Task.Delay(10000).Wait();
            channel.Close();
        }
    }
}
