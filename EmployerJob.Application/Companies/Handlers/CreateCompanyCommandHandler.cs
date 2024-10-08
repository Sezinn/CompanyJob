﻿using EmployerJob.Application.Companies.Commands;
using MediatR;
using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Repositories;
using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Common.Models.Enums;
using EmployerJob.Application.Utilities.Exceptions;
using EmployerJob.Application.Constants;

namespace EmployerJob.Application.Companies.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, BoolRef>
    {
        private readonly ICompanyRepository _companyRepository;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BoolRef> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            // Telefon numarası benzersiz olmalı
            var existingCompany = await _companyRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (existingCompany != null)
                throw new BadRequestException(ResponseCode.Exception, ErrorMessageConstant.phonenumberalreadyexist);

            var company = new Company
            {
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                Address = request.Address,
                JobPostingCredits = 2 // İlk üyelikte 2 ilan hakkı
            };

            await _companyRepository.AddAsync(company);
            var result = await _companyRepository.SaveChangesAsync();

            return result ? new BoolRef(true) : throw new BadRequestException(ResponseCode.Exception, ErrorMessageConstant.companynotcreated); 
        }
    }
}
