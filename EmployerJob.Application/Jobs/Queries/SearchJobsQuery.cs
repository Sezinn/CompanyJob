﻿using EmployerJob.Application.Jobs.Dtos;
using EmployerJob.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Jobs.Queries
{
    public class SearchJobsQuery : IRequest<IEnumerable<JobDto>>
    {
        public DateTime ExpirationDate { get; set; }
    }
}
