using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Context;
using EmployerJob.Infrastructure.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Infrastructure.Persistence.Repositories
{
    public interface IProhibitedWordRepository : IRepository<ProhibitedWord>
    {
    }

    public class ProhibitedWordRepository : Repository<ProhibitedWord>, IProhibitedWordRepository
    {
        public ProhibitedWordRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<Job>> GetActiveJobsAsync()
    }
}
