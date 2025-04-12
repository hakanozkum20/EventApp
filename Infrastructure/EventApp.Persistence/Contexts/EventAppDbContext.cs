using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities;
using EventApp.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventApp.Domain.Entities.Identity;

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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        

         public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Audit fields (CreatedDate, UpdatedDate) için otomatik değer atama
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

// DatabasePostgres": "Server=192.168.1.17;Port=5432;Database=EventPostgres;User Id=casaos;Password=casaos;