using Volo.Abp.Modularity;

namespace Miwen.Abp;

[DependsOn(
    typeof(AbpApplicationModule),
    typeof(AbpDomainTestModule)
)]
public class AbpApplicationTestModule : AbpModule
{

}
