using EventApp.Application.Validators.Companies;
using EventApp.Application.ViewModels.Companies;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Validator'ları kaydet
            services.AddScoped<IValidator<VM_Create_Company>, VM_Create_CompanyValidator>();
            services.AddScoped<IValidator<VM_Update_Company>, VM_Update_CompanyValidator>();

            // Tüm validator'ları otomatik olarak kaydet (alternatif yöntem)
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Fluent Validation'ı MVC ile entegre et
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
