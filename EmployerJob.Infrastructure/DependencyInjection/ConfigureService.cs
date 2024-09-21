using EmployerJob.Infrastructure.Persistence.Repositories;
using EmployerJob.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployerJob.Infrastructure.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Persistence.Context.ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Generic Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Specific Repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobRepository, JobRepository>();

            return services;
        }
    }
}
