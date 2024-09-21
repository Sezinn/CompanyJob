using System.Reflection;
using EmployerJob.Application.Companies.Mapping;
using EmployerJob.Application.Jobs.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmployerJob.Application.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
