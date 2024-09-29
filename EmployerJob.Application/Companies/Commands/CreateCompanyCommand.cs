using EmployerJob.Application.Common.Models.BaseModels;
using MediatR;

namespace EmployerJob.Application.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<BoolRef>
    {
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
    }
}
