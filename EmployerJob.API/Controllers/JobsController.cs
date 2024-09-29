﻿using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Jobs.Commands;
using EmployerJob.Application.Jobs.Dtos;
using EmployerJob.Application.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployerJob.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : BaseController
    {
        protected readonly IMediator mediator;
        public JobsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{companyId}")]
        public async Task<BaseResponse<BoolRef>> CreateJob(CreateJobCommand command)
        {
            var result = await mediator.Send(command);
            var httpResponse = result?.Result == true ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            return CreateDefaultResponse(result, httpResponse);
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<JobDto>>> GetJobs()
        {
            var result = await mediator.Send(new GetAllJobsQuery());
            var httpResponse = result?.Any() == true ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return result != null ? CreateDefaultResponse(result, HttpStatusCode.OK) :
                                       CreateDefaultResponse<IEnumerable<JobDto>>(null, HttpStatusCode.NotFound);
        }

        [HttpGet("search")]
        public async Task<BaseResponse<IEnumerable<JobDto>>> SearchJobs(SearchJobsQuery query)
        {
            var result = await mediator.Send(query);
            return result != null ? CreateDefaultResponse(result, HttpStatusCode.OK) :
                                       CreateDefaultResponse<IEnumerable<JobDto>>(null, HttpStatusCode.NotFound);
        }
    }
}
