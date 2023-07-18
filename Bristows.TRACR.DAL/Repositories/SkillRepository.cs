using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;
using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.DAL.Repositories
{
    public class SkillRepository : RepositoryBase<Skill, TRACRContext, SkillRepository>, ISkillRepository  //IEmployeeRepo implemented in RepoBase
    {
        public SkillRepository(IDbFactory<TRACRContext> dbFactory, ILogger<SkillRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}