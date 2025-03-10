using Miwen.Abp.SettingManagement;
using Miwen.Abp.OAuth.Github;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace LINGYUN.Abp.Tencent.SettingManagement;

[DependsOn(
    typeof(AbpOAuthModule),
    typeof(AbpOAuthGithubModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpOAuthSettingManagementModule : AbpModule
{

}