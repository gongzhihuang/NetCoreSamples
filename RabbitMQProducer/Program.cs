using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            //RSend.Send();

            //RNewTask.NewTask();

            Console.WriteLine("a");

            var factory = new ConnectionFactory()
            {
                HostName = "122.112.226.36",
                UserName = "guest",
                Password = "galp123456"
            };
            Console.WriteLine("b");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            Console.WriteLine("c");
            channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

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

            channel.Dispose();
            connection.Close();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }


    }
}
