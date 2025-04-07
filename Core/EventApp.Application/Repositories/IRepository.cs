using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Repositories
{
    public interface IRepository <T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}