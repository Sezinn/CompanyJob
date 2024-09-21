using MediatR;
using EmployerJob.Domain.Entities;
using EmployerJob.Application.Companies.Dtos;

namespace EmployerJob.Application.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>> { }
}
