using EmployerJob.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployerJob.Domain.Entities;
using EmployerJob.Application.Companies.Queries;
using EmployerJob.Infrastructure.Persistence.Repositories;
using EmployerJob.Application.Companies.Dtos;
using AutoMapper;

namespace EmployerJob.Application.Companies.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }
    }
}
