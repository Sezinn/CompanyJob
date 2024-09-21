using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Companies.Dtos
{
    public record CompanyDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; init; } 
        public string CompanyName { get; init; } 
        public string Address { get; init; } 
        public int JobPostingCredits { get; init; } 
    }
}
