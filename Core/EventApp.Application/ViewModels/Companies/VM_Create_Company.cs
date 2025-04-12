namespace EventApp.Application.ViewModels.Companies
{
    public class VM_Create_Company
    {
        public string Name { get; set; }
        public Guid? CustomerId { get; set; } // Nullable yaptık
    }
}