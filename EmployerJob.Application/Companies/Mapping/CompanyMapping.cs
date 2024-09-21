using AutoMapper;
using EmployerJob.Application.Companies.Dtos;
using EmployerJob.Domain.Entities;

namespace EmployerJob.Application.Companies.Mapping
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping() 
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
