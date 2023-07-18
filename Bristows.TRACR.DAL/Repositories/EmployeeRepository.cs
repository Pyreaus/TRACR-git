using Bristows.TRACR.Model.Models.Entities.Employees;
using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;

namespace Bristows.TRACR.DAL.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee, TRACRContext, EmployeeRepository>, IEmployeeRepository  //IEmployeeRepo implemented in RepoBase
    {
        public EmployeeRepository(IDbFactory<TRACRContext> dbFactory, ILogger<EmployeeRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}