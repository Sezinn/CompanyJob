using EmployerJob.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Infrastructure.Persistence.Configuration
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(r => r.Id);

            builder.ToTable("Jobs");

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Jobs)
                .HasForeignKey(x => x.CompanyId);

            builder.HasData(GetJobs());
        }

        private Job[] GetJobs()
        {
            return new Job[]
            {
                new Job
                {
                    Id = 1,
                    CompanyId = 1,
                    Position = "Developer",
                    Benefits = "",
                    EmploymentType = "Part Time",
                    Salary = "100000 TL",
                    Description = "Yazılım geliştirme uzmanı aramaktayız. Max 5 yıl tecrübesi olan, .Net Core ile proje geliştirmiş olması tercih sebebidir.",
                    PostedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMonths(1),
                    QualityScore = 3,
                    IsActive = true
                }
            };
        }
    }
}
