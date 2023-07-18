using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Contexts;
using Microsoft.Extensions.Logging;
using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.DAL.Repositories
{
    public class TraineeRepository : RepositoryBase<Trainee, TRACRContext, TraineeRepository>, ITraineeRepository  //IEmployeeRepo implemented in RepoBase
    {
        public TraineeRepository(IDbFactory<TRACRContext> dbFactory, ILogger<TraineeRepository> logger) : base(dbFactory, logger)
        {
        }
    }
}