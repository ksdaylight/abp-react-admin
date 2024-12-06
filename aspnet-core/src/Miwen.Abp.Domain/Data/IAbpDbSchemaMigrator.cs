using System.Threading.Tasks;

namespace Miwen.Abp.Data;

public interface IAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
