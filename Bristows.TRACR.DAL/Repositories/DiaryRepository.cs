using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;
using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.DAL.Repositories
{
    public sealed class DiaryRepository : RepositoryBase<Diary, TRACRContext, DiaryRepository>, IDiaryRepository  //IEmployeeRepo implemented in RepoBase
    {
        public DiaryRepository(IDbFactory<TRACRContext> dbFactory, ILogger<DiaryRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}