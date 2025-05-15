using EventApp.Application.Services;
using EventApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileService, FileService>();
    }
}