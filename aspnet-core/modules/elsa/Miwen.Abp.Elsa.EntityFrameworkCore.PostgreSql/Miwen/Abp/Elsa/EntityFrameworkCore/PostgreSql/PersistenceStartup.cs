using Elsa.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Miwen.Abp.Elsa.EntityFrameworkCore.PostgreSql;

[Feature("DefaultPersistence:EntityFrameworkCore:PostgreSql")]
public class PersistenceStartup : PersistenceStartupBase
{
    protected override string ProviderName => "PostgreSql";

    protected override void Configure(DbContextOptionsBuilder options, string connectionString)
    {
        options.UseNpgsql(connectionString);
    }
}
