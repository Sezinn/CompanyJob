using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EmployerJob.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ProhibitedWord> ProhibitedWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new JobConfig());
            modelBuilder.ApplyConfiguration(new ProhibitedWordConfig());
        }
    }
}
