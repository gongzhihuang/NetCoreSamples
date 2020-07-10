using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HostBuilderConsole
{
    public class MyApplication
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }
        public MyApplication(ILogger<MyApplication> logger,
            IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public async Task<string> Run()
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);


            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://baidu.com");

            var client = _httpFactory.CreateClient();
            var response = await client.SendAsync(request);

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}
