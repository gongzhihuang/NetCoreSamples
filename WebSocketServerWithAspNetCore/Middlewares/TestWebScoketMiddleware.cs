using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebSocketServerWithAspNetCore.Middlewares
{
    public class TestWebScoketMiddleware
    {
        private ILogger<TestWebScoketMiddleware> _logger;

        /// <summary>
        /// 下一级管道
        /// </summary>
        private RequestDelegate _next;

        /// <summary>
        /// URL地址后缀
        /// </summary>
        private const string routePostfix = "/testWebSocket";

        public static List<WebSocket> webSockets = new List<WebSocket>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">下一级管道</param>
        /// <param name="logger"></param>
        public TestWebScoketMiddleware(RequestDelegate next, ILogger<TestWebScoketMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!IsWebSocket(context))
            {
                await _next.Invoke(context);
                return;
            }

            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            webSockets.Add(webSocket);
            await Echo(context, webSocket);
        }

        /// <summary>
        /// 当前请求是否为WebSocket
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        private bool IsWebSocket(HttpContext context)
        {
            return context.WebSockets.IsWebSocketRequest &&
                context.Request.Path == routePostfix;
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            Console.WriteLine("-------------------------Enter Echo");

            var buffer = new byte[1024 * 4];

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                Console.WriteLine("-------------------------While");

                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            Console.WriteLine("-------------------------Will Close");

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

            webSockets.Remove(webSocket);
        }
    }
}
