using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //RReceive.Receive();

            RWorker.Worker();
        }
    }
}
