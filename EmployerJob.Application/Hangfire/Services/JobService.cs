using EmployerJob.Application.Hangfire.Services.Jobs;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;

namespace EmployerJob.Application.Hangfire.Services
{
    public static class JobService
    {
        public static void GetProhibitedWords(IConfiguration configuration)
        {
            var jobActive = configuration.GetValue<bool>("ScheduleJobStatus:GetProhibitedWords");

            if (jobActive)
            {
                RecurringJob.AddOrUpdate<IHangfireJobService>(nameof(GetProhibitedWords),
                                              (service) => service.GetProhibitedWords(), configuration.GetValue<string>("CronScheduleConfig:GetProhibitedWords"));
            }
        }
    }
}
