using EmployerJob.Application.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Utilities.Exceptions
{
    public class NotFoundException : HttpStatusException
    {
        public NotFoundException() : base(System.Net.HttpStatusCode.NotFound)
        {
        }
        public NotFoundException(string msg, Exception innerException = null) : base(System.Net.HttpStatusCode.NotFound, msg, innerException) { }
        public NotFoundException(ResponseCode errorCode) : base(System.Net.HttpStatusCode.NotFound, errorCode) { }
        public NotFoundException(ResponseCode errorCode, string msg, Exception innerException = null) : base(System.Net.HttpStatusCode.NotFound, errorCode, msg, innerException) { }
    }
}
