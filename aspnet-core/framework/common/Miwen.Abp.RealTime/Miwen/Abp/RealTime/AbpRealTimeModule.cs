using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;

namespace Miwen.Abp.RealTime;

[DependsOn(typeof(AbpEventBusAbstractionsModule))]
public class AbpRealTimeModule : AbpModule
{
}
