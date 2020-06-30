using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQProducer
{
    public class RSend
    {
        public static void Send()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "122.112.226.36",
                UserName = "guest",
                Password = "galp123456"
            };
            using (var connection = factory.CreateConnection())//创建连接
            using (var channel = connection.CreateModel())//创建通道
            {
                //队列声明
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                //发布消息
                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
