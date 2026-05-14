using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Abp.LocalizationManagement;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpLocalizationManagementDomainModule),
    typeof(AbpLocalizationManagementApplicationContractsModule))]
public class AbpLocalizationManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpLocalizationManagementApplicationModule>();
    }
}
