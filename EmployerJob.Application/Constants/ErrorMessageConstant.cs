using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Constants
{
    public static class ErrorMessageConstant
    {
        public const string phonenumberalreadyexist = "Bu telefon numarası zaten kayıtlı.";
        public const string companynotcreated = "İşveren kaydedilemedi.";
        public const string jobnotcreated = "İlanınız kaydedilemedi.";
        public const string companynotfound = "İşveren bulunamadı.";
        public const string jobnotpublished = "İlan yayınlama hakkınız kalmamıştır.";
    }
}
