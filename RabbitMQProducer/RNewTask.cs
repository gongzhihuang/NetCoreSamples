using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQProducer
{
    public class RNewTask
    {
        public static void NewTask()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "122.112.226.36",
                UserName = "guest",
                Password = "galp123456"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("exchange_name", ExchangeType.Direct, false, false, null);

                channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.QueueBind("task_queue", "exchange_name", "exchange_name", null);

                string message;
                do
                {
                    message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: "task_queue", basicProperties: properties, body: body);
                    Console.WriteLine($"发送消息: {message}");
                } while (message.Trim().ToLower() != "exit");
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
