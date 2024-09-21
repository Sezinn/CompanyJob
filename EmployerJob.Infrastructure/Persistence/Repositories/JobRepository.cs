using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Context;
using EmployerJob.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployerJob.Infrastructure.Persistence.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<IEnumerable<Job>> GetActiveJobsAsync();
        Task<IEnumerable<Job>> SearchJobsByExpirationDateAsync(DateTime expirationDate);
    }

    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Job>> GetActiveJobsAsync()
        {
            return await _context.Jobs
                                 .Include(j => j.Company)
                                 .Where(j => j.IsActive)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Job>> SearchJobsByExpirationDateAsync(DateTime expirationDate)
        {
            return await _context.Jobs
                                 .Include(j => j.Company)
                                 .Where(j => j.ExpirationDate >= expirationDate)
                                 .ToListAsync();
        }
    }
}
