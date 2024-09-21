using AutoMapper;
using EmployerJob.Application.Companies.Dtos;
using EmployerJob.Application.Jobs.Dtos;
using EmployerJob.Application.Jobs.Queries;
using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployerJob.Application.Jobs.Handlers
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public GetAllJobsQueryHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _jobRepository.Table.Include(x => x.Company).ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<JobDto>>(entities);
        }
    }
}
