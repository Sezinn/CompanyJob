using EmployerJob.Infrastructure.Persistence.Repositories;
using EmployerJob.Infrastructure.Persistence.Repositories.Generic;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployerJob.Infrastructure.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string redisConnectionString)
        {
            services.AddDbContext<Persistence.Context.ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(connectionString);
            });

            services.AddHangfireServer();
            // Generic Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Specific Repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IProhibitedWordRepository, ProhibitedWordRepository>();

            return services;
        }
    }
}
