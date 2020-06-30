using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class RWorker
    {
        public static void Worker()
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


                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine("等待消息");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"收到消息: {message}");

                    Thread.Sleep(10000);

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                    Console.WriteLine($"已发送应答: {ea.DeliveryTag}");
                };

                channel.BasicConsume(queue: "task_queue", autoAck: false, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
