using Microsoft.Extensions.Configuration;
using Nest;

namespace EmployerJob.Infrastructure.Elasticsearch
{
    public class ElasticClientProvider
    {
        private readonly IConfiguration _configuration;

        public ElasticClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IElasticClient GetElasticClient()
        {
            var settings = new ConnectionSettings(new Uri(_configuration["Elasticsearch:Uri"]))
                .DefaultIndex("jobs")
                .DefaultMappingFor<EmployerJob.Domain.Entities.Job>(m => m
                    .IdProperty(j => j.Id)
                )
                .DisableDirectStreaming()
                .PrettyJson();

            var client = new ElasticClient(settings);

            // Create index if not exists
            var existsResponse = client.Indices.Exists("jobs");
            if (!existsResponse.Exists)
            {
                client.Indices.Create("jobs", c => c
                    .Map<EmployerJob.Domain.Entities.Job>(m => m
                        .AutoMap()
                    )
                );
            }

            return client;
        }
    }
}
