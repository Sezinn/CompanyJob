using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Jobs.Commands;
using EmployerJob.Infrastructure.Persistence.Context;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace EmployerJob.Application.Jobs.Handlers
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BoolRef>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IProhibitedWordRepository _prohibitedWordRepository;
        private readonly IElasticClient _elasticClient;

        public CreateJobCommandHandler(ICompanyRepository companyRepository, IJobRepository jobRepository, IProhibitedWordRepository prohibitedWordRepository, IElasticClient elasticClient)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _prohibitedWordRepository = prohibitedWordRepository;
            _elasticClient = elasticClient;
        }

        public async Task<BoolRef> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
                throw new ArgumentException("İşveren bulunamadı.");

            if (company.JobPostingCredits <= 0)
                throw new InvalidOperationException("İlan yayınlama hakkınız kalmamıştır.");

            // İlan haklarını azalt
            company.JobPostingCredits -= 1;

            var prohibitedWords = _prohibitedWordRepository.GetAllAsync().Result.Select(pw => pw.Word).ToList();
            bool hasProhibitedWord = prohibitedWords.Any(word => request.Description.Contains(word, StringComparison.OrdinalIgnoreCase));


            var job = new EmployerJob.Domain.Entities.Job
            {
                Position = request.Position,
                Description = request.Description,
                PostedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                Benefits = request.Benefits,
                EmploymentType = request.EmploymentType,
                Salary = request.Salary,
                IsActive = true,
                QualityScore = CalculateQualityScore(request, hasProhibitedWord)
            };


            await _jobRepository.AddAsync(job);
            var result = await _jobRepository.SaveChangesAsync();

            // Elasticsearch'e ekleme
            await _elasticClient.IndexDocumentAsync(job, cancellationToken);

            return result ? new BoolRef(true) : throw new Exception("İlanınız kaydedilemedi.");
        }
        private int CalculateQualityScore(CreateJobCommand dto, bool hasProhibitedWord)
        {
            int score = 0;
            if (!string.IsNullOrEmpty(dto.EmploymentType)) score += 1;
            if (!string.IsNullOrEmpty(dto.Salary)) score += 1;
            if (!string.IsNullOrEmpty(dto.Benefits)) score += 1;

            if (!hasProhibitedWord) score += 2;

            return score;
        }
    }
}
