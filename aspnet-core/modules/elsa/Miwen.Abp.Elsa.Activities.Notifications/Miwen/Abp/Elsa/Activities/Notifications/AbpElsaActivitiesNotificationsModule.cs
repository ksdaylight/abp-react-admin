using Miwen.Abp.Notifications;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Elsa.Activities.Notifications;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpNotificationsModule))]
public class AbpElsaActivitiesNotificationsModule : AbpModule
{
}
