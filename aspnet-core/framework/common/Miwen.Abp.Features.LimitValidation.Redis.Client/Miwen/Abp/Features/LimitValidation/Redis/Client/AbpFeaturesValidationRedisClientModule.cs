using Volo.Abp.Modularity;

namespace Miwen.Abp.Features.LimitValidation.Redis.Client;

[DependsOn(typeof(AbpFeaturesValidationRedisModule))]
public class AbpFeaturesValidationRedisClientModule : AbpModule
{
}
