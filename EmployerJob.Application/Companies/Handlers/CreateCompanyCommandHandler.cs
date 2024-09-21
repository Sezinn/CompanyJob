using EmployerJob.Application.Companies.Commands;
using EmployerJob.Infrastructure.Persistence.Context;
using MediatR;
using EmployerJob.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EmployerJob.Infrastructure.Persistence.Repositories;

namespace EmployerJob.Application.Companies.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICompanyRepository _companyRepository;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            // Telefon numarası benzersiz olmalı
            var existingCompany = await _companyRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (existingCompany != null)
            {
                throw new ArgumentException("Bu telefon numarası zaten kayıtlı.");
            }

            var company = new Company
            {
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                Address = request.Address,
                JobPostingCredits = 2 // İlk üyelikte 2 ilan hakkı
            };

            await _companyRepository.AddAsync(company);
            await _companyRepository.SaveChangesAsync();

            return company.Id;
        }
    }
}
