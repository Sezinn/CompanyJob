using EmployerJob.Application.Utilities.Exceptions;
using EmployerJob.Application.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Common.Models.BaseModels
{
    public class ErrorResponse : BaseResponse<object>
    {
        public ErrorResponse(Exception exception, string message = null)
        {
            Message = string.IsNullOrEmpty(message) ? exception.Message : message;

            if (exception is HttpStatusException ex)
            {
                HttpStatusCode = ex.HttpStatusCode;
                ResponseCode = ex.ResponseCode;
            }
            else
            {
                HttpStatusCode = DefaultError.DefaultHttpStatusCode;
                ResponseCode = DefaultError.DefaultErrorCode;
            }
        }
    }
}
