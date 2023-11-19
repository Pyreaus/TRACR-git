using Bristows.TRACR.DAL.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Bristows.TRACR.Model.Contexts;
using Bristows.TRACR.DAL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Bristows.TRACR.DAL.Factories
{
    public sealed class TRACRDbFactory : Disposable, IDbFactory<TRACRContext>
    {
        TRACRContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<TRACRDbFactory> logger;
        public TRACRDbFactory(IConfiguration configuration, ILogger<TRACRDbFactory> logger, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }
        public TRACRContext Init()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TRACRContext>();

            if (environment.IsDevelopment())
            {
                var connectionString = configuration.GetConnectionString("TRACREntities");
                optionsBuilder.UseSqlServer(connectionString);
                logger.LogWarning("Using DEV connection");
            }
            else
            {
                logger.LogWarning("Using PROD connection");
                var connectionString = configuration.GetConnectionString("TRACREntities");
                optionsBuilder.UseSqlServer(connectionString);
            }
            return dbContext ??= new TRACRContext(optionsBuilder.Options);
        }
        protected override void DisposeCore()
        {
            dbContext?.Dispose(); //dispose if null
        }
    }
}
