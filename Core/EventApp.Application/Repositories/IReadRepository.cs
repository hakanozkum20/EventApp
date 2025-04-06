using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
        Task<T> GetByIdAsync(string id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);

        

    }
}