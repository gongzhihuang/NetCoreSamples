using System;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HangfireTest
{
    public static class HangfireService
    {
        public static void AddHangfireService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Hangfire services.
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddHangfire(c => c.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(new MySqlStorage(configuration.GetSection("Hangfire").Value,
            new MySqlStorageOptions()
            {
                //QueuePollInterval = TimeSpan.Zero,
                //JobExpirationCheckInterval = TimeSpan.FromHours(1), //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                //CountersAggregateInterval = TimeSpan.FromMinutes(5), //- 聚合计数器的间隔。默认为5分钟。
            })));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

        }
    }
}
