using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Localization.CultureMap;

[DependsOn(typeof(AbpAspNetCoreModule))]
public class AbpLocalizationCultureMapModule : AbpModule
{
}
