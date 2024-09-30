using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Hangfire.Services.Jobs
{
    public interface IHangfireJobService
    {
        Task GetProhibitedWords();
    }
}
