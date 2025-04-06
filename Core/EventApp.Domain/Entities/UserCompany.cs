using System;
using System.Collections.Generic;
using EventApp.Domain.Entities.Common;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Domain.Entities.Identity;
using EventApp.Domain.Entities.Enums;

namespace EventApp.Domain.Entities
{
    public class UserCompany : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyRole CompanyRole { get; set; } // Şirket içindeki rol: Admin, Moderator, Viewer
        public Company Company { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}