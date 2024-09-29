using Hangfire;
using System;

namespace EmployerJob.Application.Hangfire.Services
{
    public class JobService
    {
        public void ScheduleJob()
        {
            // Örneğin her dakika çalışacak bir job ayarlayabilirsiniz
            //RecurringJob.AddOrUpdate("example-job", () => Console.WriteLine("This is a scheduled job!"), Cron.Minutely);
        }

        public void RunOnceJob()
        {
            // Bir defa çalışacak bir job
            BackgroundJob.Enqueue(() => Console.WriteLine("This job runs once!"));
        }
    }
}
