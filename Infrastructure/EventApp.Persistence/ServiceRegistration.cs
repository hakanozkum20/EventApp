using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EventApp.Application.Repositories;
using EventApp.Persistence.Repositories;
using EventApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using FluentValidation;

namespace EventApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<EventAppDbContext>(options =>
                options.UseNpgsql(Configuration.ConnectionString));


            // Repositories
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IEventReadRepository, EventReadRepository>();
            services.AddScoped<IEventWriteRepository, EventWriteRepository>();
            services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
            services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();
            services.AddScoped<IUserCompanyReadRepository, UserCompanyReadRepository>();
            services.AddScoped<IUserCompanyWriteRepository, UserCompanyWriteRepository>();
            
            
            
        }
    }
}