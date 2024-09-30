using EmployerJob.Application.Common.Models.BaseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployerJob.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator mediator;
        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public BaseController()
        {
        }

        protected BaseResponse<T> CreateDefaultResponse<T>(T data, HttpStatusCode statusCode) where T : class
        {
            BaseResponse<T> response = new BaseResponse<T>
            {
                Data = data,
                HttpStatusCode = statusCode
            };
            return response;
        }
    }
}
