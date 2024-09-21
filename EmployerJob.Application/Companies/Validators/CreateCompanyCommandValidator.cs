using EmployerJob.Application.Companies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Companies.Validators
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Matches(@"^\d{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Firma adı zorunludur.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres zorunludur.");
        }
    }
}
