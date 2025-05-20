using EventApp.Application.Abstractions.Storage;
using EventApp.Infrastructure.Enums;
using EventApp.Infrastructure.Services.Storage;
using EventApp.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();
    }
    
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    // serviceCollection.AddScoped<IStorage, AzureBlobStorage>() 
    public static void AddStorage(this IServiceCollection serviceCollection , StorageType storageType) 
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
          
            // case StorageType.Minio:
            //     serviceCollection.AddScoped<IStorage, MinioStorage>();
            //     break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
                
            
        }
    }
}