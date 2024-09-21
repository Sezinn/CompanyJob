using MediatR;

namespace EmployerJob.Application.Jobs.Commands
{
    public class CreateJobCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public string Position { get; set; } // Zorunlu
        public string Description { get; set; } // Zorunlu
        public string Benefits { get; set; } // Opsiyonel
        public string EmploymentType { get; set; } // Opsiyonel
        public string Salary { get; set; } // Opsiyonel
    }
}
