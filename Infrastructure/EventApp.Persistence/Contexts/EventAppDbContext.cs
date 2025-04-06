using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Persistence.Contexts
{
    public class EventAppDbContext : DbContext
    {
        public EventAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }


        protected EventAppDbContext()
        {
        }
    }
}