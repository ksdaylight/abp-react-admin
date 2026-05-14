using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Miwen.Platform;

[DependsOn(
    typeof(PlatformApplicationContractModule),
    typeof(PlatformDomainModule),
    typeof(AbpDddApplicationModule))]
public class PlatformApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<PlatformApplicationModule>();
    }
}
