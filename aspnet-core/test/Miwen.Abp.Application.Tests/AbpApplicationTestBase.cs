using Volo.Abp.Modularity;

namespace Miwen.Abp;

public abstract class AbpApplicationTestBase<TStartupModule> : AbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
