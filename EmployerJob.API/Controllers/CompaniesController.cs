using EmployerJob.Application.Companies.Commands;
using EmployerJob.Application.Companies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployerJob.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCompany([FromBody] CreateCompanyCommand command)
        {
            try
            {
                var companyId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetCompanies), new { id = companyId }, new { id = companyId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(companies);
        }
    }
}
