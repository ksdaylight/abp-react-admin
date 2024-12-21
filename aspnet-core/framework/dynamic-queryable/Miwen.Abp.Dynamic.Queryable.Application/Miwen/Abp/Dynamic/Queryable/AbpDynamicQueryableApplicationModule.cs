using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Dynamic.Queryable;

[DependsOn(
    typeof(AbpDynamicQueryableApplicationContractsModule),
    typeof(AbpDddApplicationModule))]
public class AbpDynamicQueryableApplicationModule : AbpModule
{
}
