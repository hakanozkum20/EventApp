using EventApp.Application.Repositories;
using EventApp.Domain.Entities.CoachTale;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class TestFileReadRepository : ReadRepository<TestFile>, ITestFileReadRepository
{
    public TestFileReadRepository(EventAppDbContext context) : base(context)
    {
    }
}