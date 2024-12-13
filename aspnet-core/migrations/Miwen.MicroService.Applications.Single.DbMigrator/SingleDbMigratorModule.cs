using Miwen.MicroService.Applications.Single.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Miwen.MicroService.Applications.Single.DbMigrator;

[DependsOn(
    //typeof(AbpUINavigationVueVbenAdminModule),
    typeof(SingleMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
    )]
public partial class SingleDbMigratorModule : AbpModule
{

}