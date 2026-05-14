using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Abp.DataProtectionManagement;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpDataProtectionManagementApplicationContractsModule),
    typeof(AbpDataProtectionManagementDomainModule))]
public class AbpDataProtectionManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpDataProtectionManagementApplicationModule>();
    }
}
