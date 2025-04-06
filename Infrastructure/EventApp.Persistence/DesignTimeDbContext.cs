using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventApp.Persistence
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<EventAppDbContext>
    {
        public EventAppDbContext CreateDbContext(string[] args)
        {
          

            DbContextOptionsBuilder<EventAppDbContext> builder = new DbContextOptionsBuilder<EventAppDbContext>();
            builder.UseNpgsql(Configuration.ConnectionString);
            return new EventAppDbContext(builder.Options);
        }
    }
}