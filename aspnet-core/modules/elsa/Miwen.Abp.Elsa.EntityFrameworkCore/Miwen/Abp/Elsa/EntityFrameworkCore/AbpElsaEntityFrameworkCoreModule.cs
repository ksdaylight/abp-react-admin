using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Elsa.EntityFrameworkCore;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class AbpElsaEntityFrameworkCoreModule : AbpModule
{
}
