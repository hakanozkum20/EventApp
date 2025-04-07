using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using EventApp.Domain.Entities;
using EventApp.Persistence.Contexts;

namespace EventApp.Persistence.Repositories
{
    public class UserCompanyReadRepository : ReadRepository<UserCompany>, IUserCompanyReadRepository
    {
        public UserCompanyReadRepository(EventAppDbContext context) : base(context)
        {
        }
    }
}