using Volo.Abp.Modularity;
using VoloAbpPermissionManagementApplicationModule = Volo.Abp.PermissionManagement.AbpPermissionManagementApplicationModule;

namespace Miwen.Abp.PermissionManagement;

[DependsOn(
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(VoloAbpPermissionManagementApplicationModule))]
public class AbpPermissionManagementApplicationModule : AbpModule
{

}
