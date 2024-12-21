using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Exporter;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class AbpExporterApplicationContractsModule : AbpModule
{
}
