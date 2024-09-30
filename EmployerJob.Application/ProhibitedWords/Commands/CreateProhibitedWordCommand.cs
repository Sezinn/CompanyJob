using EmployerJob.Application.Common.Models.BaseModels;
using MediatR;

namespace EmployerJob.Application.ProhibitedWords.Commands
{
    public class CreateProhibitedWordCommand : IRequest<BoolRef>
    {
        public string Word { get; set; }
    }
}
