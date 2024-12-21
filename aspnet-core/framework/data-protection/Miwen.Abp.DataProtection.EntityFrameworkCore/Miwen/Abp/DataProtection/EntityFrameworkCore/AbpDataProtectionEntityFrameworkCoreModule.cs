using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.DataProtection.EntityFrameworkCore;

[DependsOn(
    typeof(AbpDataProtectionModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class AbpDataProtectionEntityFrameworkCoreModule : AbpModule
{
}
