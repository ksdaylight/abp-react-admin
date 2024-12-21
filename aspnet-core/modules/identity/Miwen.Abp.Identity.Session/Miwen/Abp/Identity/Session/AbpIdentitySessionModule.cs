using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Identity.Session;

[DependsOn(typeof(AbpCachingModule))]
public class AbpIdentitySessionModule : AbpModule
{
}
