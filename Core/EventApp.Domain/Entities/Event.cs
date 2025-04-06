using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventApp.Domain.Entities.Common;
using EventApp.Domain.Entities.Enums;

namespace EventApp.Domain.Entities
{
    public class Event : BaseEntity

    {
        public string Title { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }



    }
}
