using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Domain.Entities
{
    public class Job : BaseEntityModel, IEntity
    {
        public string Position { get; set; } 
        public string Description { get; set; } 
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int QualityScore { get; set; }
        public string Benefits { get; set; } // Opsiyonel
        public string EmploymentType { get; set; } // Opsiyonel
        public string Salary { get; set; } // Opsiyonel

        // Foreign Key
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
