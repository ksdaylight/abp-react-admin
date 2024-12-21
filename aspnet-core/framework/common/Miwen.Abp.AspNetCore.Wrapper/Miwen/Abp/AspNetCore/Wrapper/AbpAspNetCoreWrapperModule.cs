using Miwen.Abp.Wrapper;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.AspNetCore.Wrapper;

[DependsOn(
    typeof(AbpWrapperModule),
    typeof(AbpAspNetCoreModule))]
public class AbpAspNetCoreWrapperModule : AbpModule
{

}
