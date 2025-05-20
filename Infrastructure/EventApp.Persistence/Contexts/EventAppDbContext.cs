using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities;
using EventApp.Domain.Entities.CoachTale;
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
        public DbSet<Domain.Entities.CoachTale.File> Files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<TestFile> TestFiles { get; set; }
        
        

        

         public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
         {
             var datas = ChangeTracker.Entries<BaseEntity>();
;            // Audit fields (CreatedDate, UpdatedDate) için otomatik değer atama
            foreach (var data in datas )
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

// DatabasePostgres": "Server=192.168.1.17;Port=5432;Database=EventPostgres;User Id=casaos;Password=casaos;