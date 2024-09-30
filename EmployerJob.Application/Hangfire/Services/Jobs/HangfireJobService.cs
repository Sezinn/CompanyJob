using EmployerJob.Application.Redis;
using EmployerJob.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Hangfire.Services.Jobs
{
    public class HangfireJobService : IHangfireJobService
    {
        private readonly IProhibitedWordRepository _prohibitedWordRepository;
        private readonly IRedisContext redisContext;
        public HangfireJobService(IProhibitedWordRepository prohibitedWordRepository, IRedisContext redisContext)
        {
              _prohibitedWordRepository = prohibitedWordRepository;
            this.redisContext = redisContext;
        }
        public async Task GetProhibitedWords()
        {
            var redisProhibitedWords = await redisContext.GetAsync<List<string>>(1, "ProhibitionWords");
            if (redisProhibitedWords.Count > 0)
            {
                var prohibitedWords = _prohibitedWordRepository.GetAllAsync().Result.Select(pw => pw.Word).ToList();

                if (prohibitedWords.Any())
                {
                    await redisContext.SaveAsync(1, "ProhibitionWords", prohibitedWords, TimeSpan.FromSeconds(2592000));
                }
            }
        }
    }
}
