using System.Security.AccessControl;

namespace EventApp.Application.Abstractions.Storage;

public interface IStorageService : IStorage
{

    public string StorageName { get; }


}