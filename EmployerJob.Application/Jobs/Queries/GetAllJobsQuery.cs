using EmployerJob.Application.Jobs.Dtos;
using MediatR;

namespace EmployerJob.Application.Jobs.Queries
{
    public class GetAllJobsQuery : IRequest<IEnumerable<JobDto>>{ }
}
