using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.Data;

/* This is used if database provider does't define
 * IAbpDbSchemaMigrator implementation.
 */
public class NullAbpDbSchemaMigrator : IAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
