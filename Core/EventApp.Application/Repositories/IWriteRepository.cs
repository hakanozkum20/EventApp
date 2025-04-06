using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Application.Repositories
{
    public interface IWriteRepository<T>    : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveRangeAsync(List<T> models);
        Task<bool> UpdateAsync(T model);
        
    }
}