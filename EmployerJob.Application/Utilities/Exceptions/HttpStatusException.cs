using EmployerJob.Application.Common.Models.Enums;
using EmployerJob.Application.Utilities.Settings;
using System.Net;

namespace EmployerJob.Application.Utilities.Exceptions
{
    public class HttpStatusException : Exception
    {
        private const HttpStatusCode _defaultHttpStatusCode = DefaultError.DefaultHttpStatusCode;
        private const ResponseCode _defaultPisErrorCode = DefaultError.DefaultErrorCode;

        public HttpStatusCode HttpStatusCode { get; private set; }
        public ResponseCode ResponseCode { get; set; }

        public HttpStatusException() : base()
        {
            HttpStatusCode = _defaultHttpStatusCode;
            ResponseCode = _defaultPisErrorCode;
        }

        public HttpStatusException(string msg, Exception innerException = null) : base(msg, innerException)
        {
            HttpStatusCode = _defaultHttpStatusCode;
            ResponseCode = _defaultPisErrorCode;
        }
        public HttpStatusException(HttpStatusCode status) : base()
        {
            HttpStatusCode = status;
            ResponseCode = _defaultPisErrorCode;
        }

        public HttpStatusException(ResponseCode errorCode) : base()
        {
            HttpStatusCode = _defaultHttpStatusCode;
            ResponseCode = errorCode;
        }

        public HttpStatusException(HttpStatusCode status, string msg, Exception innerException = null) : base(msg, innerException)
        {
            HttpStatusCode = status;
            ResponseCode = _defaultPisErrorCode;
        }

        public HttpStatusException(ResponseCode errorCode, string msg, Exception innerException = null) : base(msg, innerException)
        {
            HttpStatusCode = _defaultHttpStatusCode;
            ResponseCode = errorCode;
        }

        public HttpStatusException(HttpStatusCode status, ResponseCode errorCode) : base()
        {
            HttpStatusCode = status;
            ResponseCode = errorCode;
        }

        public HttpStatusException(HttpStatusCode status, ResponseCode errorCode, string msg, Exception innerException = null) : base(msg, innerException)
        {
            HttpStatusCode = status;
            ResponseCode = errorCode;
        }
    }
}
