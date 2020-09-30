using System;
namespace HangfireTest.Jobs
{
    public class TestJob : ITestJob
    {
        public TestJob()
        {
        }

        public void TestBackgroundMethods()
        {
            Console.WriteLine("测试 TestBackgroundMethods " + DateTime.Now);
        }
    }
}
