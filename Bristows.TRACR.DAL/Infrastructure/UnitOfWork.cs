using System;
using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bristows.TRACR.DAL.Infrastructure
{
    public abstract class UnitOfWork<TR> where TR : DbContext
    {
        private readonly IDbFactory<TR> dbFactory;
        private TR dbContext;

        protected UnitOfWork(IDbFactory<TR> dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public TR DbContext
        {
            get { return dbContext ??= dbFactory.Init(); }
        }
        public int Commit()
        {
            return DbContext.SaveChanges();
        }
    }
}