using System;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace QuartzTest
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<QuartzService> _logger;
        
        public JobFactory(IServiceProvider serviceProvider, ILogger<QuartzService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var job = _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
