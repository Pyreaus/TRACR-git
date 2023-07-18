using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;
using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.DAL.Repositories
{
    public class PeopleRepository : RepositoryBase<PeopleFinderUser, TRACRContext, PeopleRepository>, IPeopleRepository  //IEmployeeRepo implemented in RepoBase
    {
        public PeopleRepository(IDbFactory<TRACRContext> dbFactory, ILogger<PeopleRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}