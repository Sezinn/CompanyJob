using EmployerJob.Application.Companies.Dtos;
using EmployerJob.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Jobs.Dtos
{
    public record JobDto
    {
        public string Position { get; init; }
        public string Description { get; init; }
        public DateTime PostedDate { get; init; }
        public DateTime ExpirationDate { get; init; }
        public int QualityScore { get; init; }
        public string Benefits { get; init; } // Opsiyonel
        public string EmploymentType { get; init; } // Opsiyonel
        public string Salary { get; init; } // Opsiyonel
        public bool IsActive { get; init; }

        // Foreign Key
        public CompanyDto Company { get; init; }
    }
}
