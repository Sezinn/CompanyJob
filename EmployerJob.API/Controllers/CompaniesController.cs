using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Companies.Commands;
using EmployerJob.Application.Companies.Dtos;
using EmployerJob.Application.Companies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployerJob.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : BaseController
    {
        protected readonly IMediator mediator;
        public CompaniesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<BoolRef>> RegisterCompany(CreateCompanyCommand command)
        {
            var result = await mediator.Send(command);
            var httpResponse = result?.Result == true ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            return CreateDefaultResponse(result, httpResponse);
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var result = await mediator.Send(new GetAllCompaniesQuery());
            var httpResponse = result?.Any() == true ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return result != null ? CreateDefaultResponse(result, HttpStatusCode.OK) :
                                       CreateDefaultResponse<IEnumerable<CompanyDto>>(null, HttpStatusCode.NotFound);
        }
    }
}
