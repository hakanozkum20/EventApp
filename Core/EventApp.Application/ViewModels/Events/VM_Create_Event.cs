
namespace EventApp.Application.ViewModels.Events
{
    public class VM_Create_Event
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public Guid CompanyId { get; set; }
    }
}