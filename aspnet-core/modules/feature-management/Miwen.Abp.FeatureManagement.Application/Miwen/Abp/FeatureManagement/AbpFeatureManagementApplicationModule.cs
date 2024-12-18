using Volo.Abp.Modularity;
using VoloAbpFeatureManagementApplicationModule = Volo.Abp.FeatureManagement.AbpFeatureManagementApplicationModule;

namespace Miwen.Abp.FeatureManagement;

[DependsOn(
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(VoloAbpFeatureManagementApplicationModule))]
public class AbpFeatureManagementApplicationModule : AbpModule
{
}
