using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Miwen.Abp.BackgroundTasks.EventBus;

[DependsOn(typeof(AbpEventBusModule))]
[DependsOn(typeof(AbpBackgroundTasksModule))]
public class AbpBackgroundTasksEventBusModule : AbpModule
{
}
