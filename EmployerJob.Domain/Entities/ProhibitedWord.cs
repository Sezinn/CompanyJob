using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Domain.Entities
{
    public class ProhibitedWord : IEntity
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
