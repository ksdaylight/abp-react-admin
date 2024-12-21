using Miwen.Abp.Webhooks;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Elsa.Activities.Webhooks;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpWebhooksModule))]
public class AbpElsaActivitiesWebhooksModule : AbpModule
{
}
