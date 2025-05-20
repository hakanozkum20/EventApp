using EventApp.Application.Repositories;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class FileWriteRepository : WriteRepository<Domain.Entities.CoachTale.File>, IFileWriteRepository
{
    public FileWriteRepository(EventAppDbContext context) : base(context)
    {
    }
}