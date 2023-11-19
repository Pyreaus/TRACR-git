using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Bristows.TRACR.Model.Contexts;

namespace Bristows.TRACR.DAL.Factories
{
    public sealed class TRACRWorkUnit : UnitOfWork<TRACRContext>
    {
        public TRACRWorkUnit(IDbFactory<TRACRContext> dbFactory) : base(dbFactory)
        {
        }
    }
}