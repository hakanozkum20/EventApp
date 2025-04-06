using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventApp.Domain.Entities.Common;

namespace EventApp.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<UserCompany> UserCompanies { get; set; }
        

    }
}
