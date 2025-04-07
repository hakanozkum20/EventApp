using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using EventApp.Domain.Entities.Common;
using EventApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity 
    {
        private readonly EventAppDbContext _context;
        public ReadRepository(EventAppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)   
        => !tracking ? Table.AsQueryable().AsNoTracking() : Table.AsQueryable(); 

        
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        => !tracking ? Table.Where(method).AsNoTracking() : Table.Where(method); 


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //  => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        } 

       

    }
}