using Miwen.Abp.IdGenerator.Snowflake;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace Miwen.Abp.IdGenerator;

public class AbpIdGeneratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var snowflakeIdOptions = new SnowflakeIdOptions();
        context.Services.ExecutePreConfiguredActions(snowflakeIdOptions);

        context.Services.TryAddSingleton<IDistributedIdGenerator>(SnowflakeIdGenerator.Create(snowflakeIdOptions));
    }
}
