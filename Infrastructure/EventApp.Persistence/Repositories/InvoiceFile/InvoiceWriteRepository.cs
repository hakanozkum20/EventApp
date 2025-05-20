using EventApp.Application.Repositories;
using EventApp.Domain.Entities.CoachTale;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class InvoiceWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceWriteRepository(EventAppDbContext context) : base(context)
    {
    }
}