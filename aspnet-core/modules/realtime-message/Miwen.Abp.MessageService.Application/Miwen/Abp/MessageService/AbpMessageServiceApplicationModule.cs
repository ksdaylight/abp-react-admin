using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Miwen.Abp.MessageService;

[DependsOn(
    typeof(AbpMessageServiceApplicationContractsModule),
    typeof(AbpMessageServiceDomainModule))]
public class AbpMessageServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpMessageServiceApplicationModule>();
    }
}
