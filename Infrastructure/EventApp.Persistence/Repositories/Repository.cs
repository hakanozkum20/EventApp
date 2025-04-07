using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using EventApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table => throw new NotImplementedException();
    }
}