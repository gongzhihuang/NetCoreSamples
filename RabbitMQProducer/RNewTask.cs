using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQProducer
{
    public class RNewTask
    {
        public static void NewTask()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            string exchangeName = "exchangeName";
            string queueName = "queueName";
            string routeKey = "routeKey";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);

                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.QueueBind(queueName, exchangeName, routeKey, null);

                Console.WriteLine($"就绪");

                string message;
                do
                {
                    message = Console.ReadLine();

                    //Message message1 = new Message
                    //{
                    //    ProjectId = "111",
                    //    Token = "xxxxxxxxxxxx",
                    //};

                    //var str = JsonConvert.SerializeObject(message1);
                    //var body = Encoding.UTF8.GetBytes(str);

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties: properties, body: body);
                    Console.WriteLine($"发送消息: {message}");
                } while (message.Trim().ToLower() != "exit");
            }
            Console.ReadLine();
        }
    }
}
