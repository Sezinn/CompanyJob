using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Common.Models.Enums;
using EmployerJob.Application.Constants;
using EmployerJob.Application.Jobs.Commands;
using EmployerJob.Application.Redis;
using EmployerJob.Application.Utilities.Exceptions;
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
        private readonly IRedisContext redisContext;

        public CreateJobCommandHandler(ICompanyRepository companyRepository, IJobRepository jobRepository, IProhibitedWordRepository prohibitedWordRepository, IElasticClient elasticClient, IRedisContext redisContext)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _prohibitedWordRepository = prohibitedWordRepository;
            _elasticClient = elasticClient;
            this.redisContext = redisContext;
        }

        public async Task<BoolRef> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
                throw new NotFoundException(ResponseCode.Exception, ErrorMessageConstant.companynotfound);

            if (company.JobPostingCredits <= 0)
                throw new BadRequestException(ResponseCode.Exception, ErrorMessageConstant.jobnotpublished); 

            // İlan haklarını azalt
            company.JobPostingCredits -= 1;

            var prohibitedWords = await redisContext.GetAsync<List<string>>(1, "ProhibitionWords");
            bool hasProhibitedWord = prohibitedWords.Any(word => request.Description.Contains(word, StringComparison.OrdinalIgnoreCase));


            var job = new EmployerJob.Domain.Entities.Job
            {
                Position = request.Position,
                Description = request.Description,
                PostedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(15),
                Benefits = request.Benefits,
                EmploymentType = request.EmploymentType,
                Salary = request.Salary,
                IsActive = true,
                CompanyId = company.Id,
                QualityScore = CalculateQualityScore(request, hasProhibitedWord)
            };


            await _jobRepository.AddAsync(job);
            _companyRepository.Update(company);

            var result = await _companyRepository.SaveChangesAsync();

            // Elasticsearch'e ekleme
            await _elasticClient.IndexDocumentAsync(job, cancellationToken);

            return result ? new BoolRef(true) : throw new BadRequestException(ResponseCode.Exception, ErrorMessageConstant.jobnotcreated);
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
