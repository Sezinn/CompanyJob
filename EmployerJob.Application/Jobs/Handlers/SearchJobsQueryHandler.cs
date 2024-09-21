using EmployerJob.Application.Jobs.Queries;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;
using Nest;

namespace EmployerJob.Application.Jobs.Handlers
{
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, IEnumerable<EmployerJob.Domain.Entities.Job>>
    {
        private readonly IElasticClient _elasticClient;
        public SearchJobsQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<IEnumerable<EmployerJob.Domain.Entities.Job>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            var searchResponse = await _elasticClient.SearchAsync<EmployerJob.Domain.Entities.Job>(s => s
                .Query(q => q
                    .DateRange(dr => dr
                        .Field(f => f.ExpirationDate)
                        .GreaterThanOrEquals(request.ExpirationDate)
                    )
                )
            , cancellationToken);

            return searchResponse.Documents;
        }
    }
}
