using Volo.Abp.Modularity;
using Volo.Abp.Emailing;

namespace Miwen.Abp.Elsa.Activities.Emailing;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpEmailingModule))]
public class AbpElsaActivitiesEmailingModule : AbpModule
{
}
