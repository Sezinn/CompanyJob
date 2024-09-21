using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Context;
using EmployerJob.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Infrastructure.Persistence.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetByPhoneNumberAsync(string phoneNumber);
    }

    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Company> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
    }
}
