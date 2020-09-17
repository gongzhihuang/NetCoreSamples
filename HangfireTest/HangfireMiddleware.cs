using System;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using HangfireTest.Jobs;
using Microsoft.AspNetCore.Builder;

namespace HangfireTest
{
    public static class HangfireMiddleware
    {
        public static void UseHangfireMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseHangfireServer(ConfigureOptions());//配置服务
            app.UseHangfireDashboard("/hangfire", HfAuthor());//配置面板
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            //backgroundJobs.AddOrUpdate();
            HangfireService();//配置各个任务
        }
        /// <summary>
        /// 配置账号模板信息 
        /// </summary>
        /// <returns></returns>
        public static DashboardOptions HfAuthor()
        {

            var filter = new BasicAuthAuthorizationFilter(
            new BasicAuthAuthorizationFilterOptions
            {
                SslRedirect = false,
                RequireSsl = false,
                LoginCaseSensitive = false,
                Users = new[]
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = "admin", //可视化的登陆账号
                        PasswordClear = "admin" //可视化的密码
                    }
                }
            });
            return new DashboardOptions
            {
                Authorization = new[] { filter }
            };
        }

        /// <summary>
        /// 配置启动
        /// </summary>
        /// <returns></returns>
        public static BackgroundJobServerOptions ConfigureOptions()
        {
            return new BackgroundJobServerOptions
            {
                Queues = new[] { "myqueue", "default" },
                WorkerCount = Environment.ProcessorCount * 5,
                ServerName = "hangfireServerName",

            };
        }

        #region 配置服务
        public static void HangfireService()
        {

            RecurringJob.AddOrUpdate<ITestJob>(s => s.TestBackgroundMethods(), "0/10 * * * * ? ", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<ITestJob>(s => s.TestBackgroundMethods(), "0/10 * * * * ? ", TimeZoneInfo.Local, "myqueue");
        }
        #endregion
    }
}
