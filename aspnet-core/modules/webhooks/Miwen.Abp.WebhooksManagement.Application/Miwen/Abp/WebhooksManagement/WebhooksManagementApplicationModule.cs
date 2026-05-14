using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Miwen.Abp.WebhooksManagement;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpDddApplicationModule),
    typeof(WebhooksManagementDomainModule))]
public class WebhooksManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<WebhooksManagementApplicationModule>();
    }
}
