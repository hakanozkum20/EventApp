using EventApp.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EventApp.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, useAsync: false);
            await fileStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    } 
    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);


        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            await CopyFileAsync($"{uploadPath}\\{file.Name}",file);
            datas.Add((file.Name,$"{path}\\{file.Name}"));
            
            
        }
     //todo Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata alındığına dair uyarıcı bir exception oluşturulup fırlatılması gerekiyor!
        return datas;
    }

    public async Task DeleteAsync(string path, string fileName)
    
        =>   File.Delete($"{path}\\{fileName}");
    

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f=> f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");
}