using System;
namespace OptionDemo.Extensions
{
    public class RabbitMQConfiguration
    {
        public string RabbitHost { get; set; }

        public string RabbitUserName { get; set; }

        public string RabbitPassword { get; set; }

        public int RabbitPort { get; set; }

        public string ExchangeName { get; set; }

        public string QueueName { get; set; }

        public string RouteKey { get; set; }
    }
}
