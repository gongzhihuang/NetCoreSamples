using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundServiceConsole
{
    public class TimedBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public TimedBackgroundService(ILogger<TimedBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation($"Hello World! - {DateTime.Now}");
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
    }
}
