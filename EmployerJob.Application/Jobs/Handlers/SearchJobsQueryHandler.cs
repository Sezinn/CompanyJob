using AutoMapper;
using EmployerJob.Application.Jobs.Dtos;
using EmployerJob.Application.Jobs.Queries;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;
using Nest;

namespace EmployerJob.Application.Jobs.Handlers
{
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        public SearchJobsQueryHandler(IElasticClient elasticClient, IMapper mapper)
        {
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        public async Task<IEnumerable<JobDto>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            var searchResponse = await _elasticClient.SearchAsync<EmployerJob.Domain.Entities.Job>(s => s
                .Query(q => q
                    .DateRange(dr => dr
                        .Field(f => f.ExpirationDate)
                        .GreaterThanOrEquals(request.ExpirationDate)
                    )
                )
            , cancellationToken);

            return _mapper.Map<IEnumerable<JobDto>>(searchResponse?.Documents);
        }
    }
}
