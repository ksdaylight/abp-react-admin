using Miwen.Abp.IM;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Elsa.Activities.IM;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpIMModule))]
public class AbpElsaActivitiesIMModule : AbpModule
{
}
