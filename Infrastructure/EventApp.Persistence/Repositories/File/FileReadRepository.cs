using EventApp.Application.Repositories;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class FileReadRepository : ReadRepository<Domain.Entities.CoachTale.File>, IFileReadRepository
{
    public FileReadRepository(EventAppDbContext context) : base(context)
    {
    }
}