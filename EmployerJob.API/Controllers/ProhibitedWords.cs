using EmployerJob.API.Controllers.Base;
using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Companies.Commands;
using EmployerJob.Application.ProhibitedWords.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployerJob.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProhibitedWords : BaseController
    {
        public ProhibitedWords(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<BaseResponse<BoolRef>> CreateProhibitedWord(CreateProhibitedWordCommand command)
        {
            var result = await mediator.Send(command);
            var httpResponse = result?.Result == true ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            return CreateDefaultResponse(result, httpResponse);
        }
    }
}
