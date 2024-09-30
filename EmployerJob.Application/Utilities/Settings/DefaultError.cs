using EmployerJob.Application.Common.Models.Enums;
using System.Net;

namespace EmployerJob.Application.Utilities.Settings
{
    public static class DefaultError
    {
        public const HttpStatusCode DefaultHttpStatusCode = HttpStatusCode.InternalServerError;
        public const ResponseCode DefaultErrorCode = ResponseCode.Exception;
    }
}
