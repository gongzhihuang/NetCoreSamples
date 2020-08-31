using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace WebSocketClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var exitEvent = new ManualResetEvent(false);
            //var url = new Uri("wss://localhost:5001/api/Test/stream");
            var url = new Uri("wss://localhost:27926/testWebSocket");

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

                client.Start().Wait();

                //Task.Run(() => StartSendingPing(client));

                exitEvent.WaitOne();

            }
        }

        private static async Task StartSendingPing(IWebsocketClient client)
        {
            while (true)
            {
                await Task.Delay(5000);

                if (!client.IsRunning)
                    continue;

                client.Send($"ping{DateTime.Now.Second}");
                Console.WriteLine($"ping{DateTime.Now.Second}");
            }
        }

    }
}
