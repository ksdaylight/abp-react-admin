using Miwen.Abp.Elasticsearch;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Logging.Serilog.Elasticsearch;

[DependsOn(
    typeof(AbpLoggingModule),
    typeof(AbpElasticsearchModule),
    typeof(AbpMapperlyModule),
    typeof(AbpJsonModule))]
public class AbpLoggingSerilogElasticsearchModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpLoggingSerilogElasticsearchOptions>(configuration.GetSection("Logging:Serilog:Elasticsearch"));

        context.Services.AddMapperlyObjectMapper<AbpLoggingSerilogElasticsearchModule>();
    }
}
