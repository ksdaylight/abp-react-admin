using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Dapr;

[DependsOn(typeof(AbpJsonModule))]
public class AbpDaprModule : AbpModule
{
}
