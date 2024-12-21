using Miwen.Abp.Notifications.Common;
using Volo.Abp.Modularity;

namespace Miwen.Abp.ExceptionHandling.Notifications;

[DependsOn(
    typeof(AbpExceptionHandlingModule),
    typeof(AbpNotificationsCommonModule))]
public class AbpNotificationsExceptionHandlingModule : AbpModule
{
}
