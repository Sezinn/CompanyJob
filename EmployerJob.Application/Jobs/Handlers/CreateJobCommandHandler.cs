using EmployerJob.Application.Jobs.Commands;
using EmployerJob.Infrastructure.Persistence.Context;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;
using Nest;

namespace EmployerJob.Application.Jobs.Handlers
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IElasticClient _elasticClient;
        //private readonly HashSet<string> _prohibitedWords = new HashSet<string> { "sakıncalı1", "sakıncalı2" }; // Örnek kelimeler

        public CreateJobCommandHandler(ICompanyRepository companyRepository, IJobRepository jobRepository, IElasticClient elasticClient)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _elasticClient = elasticClient;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
                throw new ArgumentException("İşveren bulunamadı.");

            if (company.JobPostingCredits <= 0)
                throw new InvalidOperationException("İlan yayınlama hakkınız kalmamıştır.");

            // İlan haklarını azalt
            company.JobPostingCredits -= 1;

            var job = new EmployerJob.Domain.Entities.Job
            {
                Position = request.Position,
                Description = request.Description,
                PostedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                Benefits = request.Benefits,
                EmploymentType = request.EmploymentType,
                Salary = request.Salary,
                IsActive = true
            };

            // Kalite puanı hesaplama
            job.QualityScore = CalculateQualityScore(request);

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            // Elasticsearch'e ekleme
            await _elasticClient.IndexDocumentAsync(job, cancellationToken);

            return job.Id;
        }
        private int CalculateQualityScore(CreateJobCommand dto)
        {
            int score = 0;
            if (!string.IsNullOrEmpty(dto.EmploymentType)) score += 1;
            if (!string.IsNullOrEmpty(dto.Salary)) score += 1;
            if (!string.IsNullOrEmpty(dto.Benefits)) score += 1;

            // Sakıncalı kelime kontrolü
            //bool hasProhibitedWord = _prohibitedWords.Any(word => dto.Description.Contains(word, StringComparison.OrdinalIgnoreCase));
            //if (!hasProhibitedWord) score += 2;

            return score;
        }
    }
}
