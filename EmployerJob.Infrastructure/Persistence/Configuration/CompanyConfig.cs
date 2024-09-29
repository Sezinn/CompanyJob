using EmployerJob.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployerJob.Infrastructure.Persistence.Configuration
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(c => c.PhoneNumber)
                .IsUnique();

            builder.ToTable("Companies");

            builder.HasMany(c => c.Jobs)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyId);

            builder.HasData(GetCompanies());
        }

        private Company[] GetCompanies()
        {
            return new Company[]
            {
                new Company
                {
                    Id = 1,
                    PhoneNumber = "5555555555",
                    Address = "İstanbul",
                    CompanyName = "Experilabs",
                    JobPostingCredits = 2,
                }
            };
        }
    }
}
