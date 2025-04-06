using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace EventApp.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserCompany> UserCompanies { get; set; } 
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Role SystemRole { get; set; } // SuperAdmin için

    }
}