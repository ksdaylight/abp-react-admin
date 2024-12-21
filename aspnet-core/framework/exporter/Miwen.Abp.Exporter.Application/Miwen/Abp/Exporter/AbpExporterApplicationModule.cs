using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Exporter;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpExporterCoreModule),
    typeof(AbpExporterApplicationContractsModule))]
public class AbpExporterApplicationModule : AbpModule
{
}
