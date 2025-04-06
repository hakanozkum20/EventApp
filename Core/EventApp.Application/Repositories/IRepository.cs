using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Repositories
{
    public interface IRepository <T> where T : class
    {
        DbSet<T> Table { get; }
    }
}