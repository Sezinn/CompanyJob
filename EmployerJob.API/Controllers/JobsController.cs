using EmployerJob.Application.Jobs.Commands;
using EmployerJob.Application.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployerJob.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{companyId}")]
        public async Task<IActionResult> CreateJob(int companyId, [FromBody] CreateJobCommand command)
        {
            try
            {
                command.CompanyId = companyId;
                var jobId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetJobs), new { id = jobId }, new { id = jobId });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _mediator.Send(new GetAllJobsQuery());
            return Ok(jobs);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchJobs([FromQuery] DateTime expirationDate)
        {
            var query = new SearchJobsQuery { ExpirationDate = expirationDate };
            var jobs = await _mediator.Send(query);
            return Ok(jobs);
        }
    }
}
