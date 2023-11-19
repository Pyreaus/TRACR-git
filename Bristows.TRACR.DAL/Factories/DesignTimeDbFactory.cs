using Bristows.TRACR.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bristows.TRACR.DAL.Factories
{
    public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TRACRContext>
    {
        TRACRContext dbContext;
        public TRACRContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Bristows.TRACR.DAL", "Infrastructure", "connectionConfig.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath)
                .AddJsonFile($"connectionConfig.{environmentName}.json", optional: true) // Additional environment-specific configuration
                .Build();

            var builder = new DbContextOptionsBuilder<TRACRContext>();

            if (environmentName == "Development")
            {
                var connectionString = configuration.GetConnectionString("TRACREntities");
                builder.UseSqlServer(connectionString, opts => opts.MigrationsAssembly("Bristows.TRACR.DAL"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("TRACREntities");
                builder.UseSqlServer(connectionString, opts => opts.MigrationsAssembly("Bristows.TRACR.DAL"));
            }
            return dbContext ??= new TRACRContext(builder.Options);
        }
    }
}
