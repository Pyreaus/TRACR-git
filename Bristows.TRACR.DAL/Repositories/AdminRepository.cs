using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;

namespace Bristows.TRACR.DAL.Repositories
{
    public class AdminRepository : RepositoryBase<Admin, TRACRContext, AdminRepository>, IAdminRepository  //IEmployeeRepo implemented in RepoBase
    {
        public AdminRepository(IDbFactory<TRACRContext> dbFactory, ILogger<AdminRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}