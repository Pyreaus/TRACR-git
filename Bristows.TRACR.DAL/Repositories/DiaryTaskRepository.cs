using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;
using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.DAL.Repositories
{
    public class DiaryTaskRepository : RepositoryBase<DiaryTask, TRACRContext, DiaryTaskRepository>, IDiaryTaskRepository  //IEmployeeRepo implemented in RepoBase
    {
        public DiaryTaskRepository(IDbFactory<TRACRContext> dbFactory, ILogger<DiaryTaskRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}