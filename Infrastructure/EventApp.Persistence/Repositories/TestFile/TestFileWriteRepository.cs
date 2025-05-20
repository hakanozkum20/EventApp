using EventApp.Application.Repositories;
using EventApp.Domain.Entities.CoachTale;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories;

public class TestFileWriteRepository : WriteRepository<TestFile>, ITestFileWriteRepository
{
    public TestFileWriteRepository(EventAppDbContext context) : base(context)
    {
    }
}