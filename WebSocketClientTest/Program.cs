using System;
using System.Threading;
using Websocket.Client;

namespace WebSocketClientTest
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var exitEvent = new ManualResetEvent(false);
            var url = new Uri("wss://localhost:5001/api/Test/stream");

            using (var client = new WebsocketClient(url))
            {
                client.ReconnectTimeout = TimeSpan.FromSeconds(300);
                client.ReconnectionHappened.Subscribe(info =>
                {
                    Console.WriteLine($"重连,type: {info.Type}");
                });

                client.MessageReceived.Subscribe(msg =>
                {
                    Console.WriteLine($"收到消息: {msg}");
                });

                await client.Start();

                exitEvent.WaitOne();
            }
        }
    }
}
