using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Domain.Entities
{
    public class Company : BaseEntityModel, IEntity
    {
        public string PhoneNumber { get; set; } // Zorunlu
        public string CompanyName { get; set; } // Zorunlu
        public string Address { get; set; } // Zorunlu
        public int JobPostingCredits { get; set; } // Zorunlu

        public ICollection<Job> Jobs { get; set; }
    }
}
