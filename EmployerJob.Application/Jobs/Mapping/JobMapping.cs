using AutoMapper;
using EmployerJob.Application.Jobs.Dtos;
using EmployerJob.Domain.Entities;

namespace EmployerJob.Application.Jobs.Mapping
{
    public class JobMapping : Profile
    {
        public JobMapping() 
        {
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
