using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Miwen.MicroService.Applications.Single.EntityFrameworkCore;

public class SingleMigrationsDbContextFactory : IDesignTimeDbContextFactory<SingleMigrationsDbContext>
{
    public SingleMigrationsDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        //AbpEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<SingleMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new SingleMigrationsDbContext(builder.Options);
    }


    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Miwen.MicroService.Applications.Single.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true);

        return builder.Build();
    }
}