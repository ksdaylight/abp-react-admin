using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Identity;

[DependsOn(
    typeof(Volo.Abp.Identity.AbpIdentityApplicationContractsModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpAuthorizationModule)
    )]
public class AbpIdentityApplicationContractsModule : AbpModule
{
}
