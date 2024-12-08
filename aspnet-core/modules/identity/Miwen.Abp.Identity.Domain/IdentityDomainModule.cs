using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Identity;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityDomainSharedModule)
)]
public class IdentityDomainModule : AbpModule
{

}
