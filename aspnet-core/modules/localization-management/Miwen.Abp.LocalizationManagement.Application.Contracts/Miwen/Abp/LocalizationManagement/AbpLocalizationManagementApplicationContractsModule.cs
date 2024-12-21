using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Miwen.Abp.LocalizationManagement;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpLocalizationManagementDomainSharedModule))]
public class AbpLocalizationManagementApplicationContractsModule : AbpModule
{

}
