using EventApp.Application.Repositories;
using EventApp.Domain.Entities.CoachTale;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class InvoiceReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceReadRepository(EventAppDbContext context) : base(context)
    {
    }
}