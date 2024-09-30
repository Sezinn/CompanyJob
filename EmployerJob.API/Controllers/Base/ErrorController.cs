using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.Utilities;
using EmployerJob.Application.Utilities.Exceptions;
using EmployerJob.Application.Utilities.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployerJob.API.Controllers.Base
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {

        [Route("error")]
        public BaseResponse<object> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = (int)DefaultError.DefaultHttpStatusCode;

            if (exception is HttpStatusException ex)
                code = (int)ex.HttpStatusCode;

            Response.StatusCode = code;

            var fileName = Utility.GetFileName(exception);
            var exceptionTypeName = Utility.GetExceptionTypeName(exception);
            var message = exception.Message;
            var stackTrace = exception.ToString();

            var result = new ErrorResponse(exception, message);

            return result;
        }
    }
}
