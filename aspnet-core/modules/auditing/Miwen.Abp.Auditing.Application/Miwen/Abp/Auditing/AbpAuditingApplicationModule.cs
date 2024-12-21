using Miwen.Abp.AuditLogging;
using Miwen.Abp.Logging;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Auditing;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpLoggingModule),
    typeof(AbpAuditLoggingModule),
    typeof(AbpAuditingApplicationContractsModule))]
public class AbpAuditingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpAuditingApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<AbpAuditingMapperProfile>(validate: true);
        });
    }
}
