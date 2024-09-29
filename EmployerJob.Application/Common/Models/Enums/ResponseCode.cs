using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Common.Models.Enums
{
    public enum ResponseCode : byte
    {
        Success = 0,
        Exception = 1,
        ValidationException = 2,
        CustomerServiceException = 3,
        TokenException = 4,
        BsnBaseCustomerApiException = 5,
        Unauthorized = 6,
        InsertException = 7,
        CalculateException = 8,
        SftpException = 9,
        PortalServiceException = 10,
        FileExceptions = 11,
        LoadTestException = 12,
        PaymentException = 13,
        ExceptionSendMail = 14,
        PaymentApiException = 15,
        FilterException = 16,
        AuthException = 17,
        Forbidden = 18
    }
}
