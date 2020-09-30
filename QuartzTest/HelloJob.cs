using System;
using System.Threading.Tasks;
using Quartz;

namespace QuartzTest
{
    public class HelloJob : IJob
    {
        //private readonly ITResponseXmlService _xmlcontentService; //这是我的服务层测试能否注入


        //public hellJob(ITResponseXmlService xmlcontentService)
        //{
        //    _xmlcontentService = xmlcontentService;
        //    Console.Out.WriteLineAsync("Greetings from HelloJob!");

        //}

        public async Task Execute(IJobExecutionContext context)
        {
            //_xmlcontentService = context.JobDetail.Key.Name

            //await _xmlcontentService.DownloadFtp();

            await Console.Out.WriteLineAsync("Greetings from HelloJob! " + DateTime.Now);

        }
    }
}
