using EmployerJob.Application.Jobs.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Jobs.Validators
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Geçerli bir şirket ID'si girilmelidir.");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Pozisyon zorunludur.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("İlan açıklaması zorunludur.");
        }
    }
}
