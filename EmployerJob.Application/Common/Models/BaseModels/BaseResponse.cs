using EmployerJob.Application.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Common.Models.BaseModels
{
    public class BaseResponse<T> where T : class
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpStatusCodeName => HttpStatusCode.ToString();
        public ResponseCode ResponseCode { get; set; }
        public string ResponseCodeName => ResponseCode.ToString();
        public string ExternalResponseCodeName { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string ResultMessage { get; set; }
    }
}
