using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace QuartzTest
{
    public class QuartzService : BackgroundService
    {
        private readonly IScheduler _scheduler;

        private readonly IServiceProvider _serviceProvider;
        public QuartzService(IScheduler scheduler, IServiceProvider serviceProvider)
        {
            _scheduler = scheduler;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            IJobDetail job = JobBuilder.Create<HelloJob>()
                            .WithIdentity("job", "group")
                            .Build();

            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                                       .WithIdentity("job", "group")
                                       .WithCronSchedule("*/5 * * * * ?")
                                       .Build();

            await _scheduler.ScheduleJob(job, cronTrigger);

            await _scheduler.Start(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
