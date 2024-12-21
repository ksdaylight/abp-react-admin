using Miwen.Abp.AspNetCore.Wrapper;
using Volo.Abp.Modularity;

namespace Miwen.Abp.AspNetCore.Mvc.Idempotent.Wrapper;

[DependsOn(
    typeof(AbpAspNetCoreWrapperModule),
    typeof(AbpAspNetCoreMvcIdempotentModule))]
public class AbpAspNetCoreMvcIdempotentWrapperModule : AbpModule
{

}
