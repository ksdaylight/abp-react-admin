using Miwen.Abp.SettingManagement;
using Miwen.Abp.OAuth.Localization;
using Miwen.Abp.OAuth.Github.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ValueType = Miwen.Abp.SettingManagement.ValueType;

namespace Miwen.Abp.OAuth.SettingManagement;

public class OAuthSettingAppService : ApplicationService, IOAuthSettingAppService
{
    protected ISettingManager SettingManager { get; }
    protected IPermissionChecker PermissionChecker { get; }
    protected ISettingDefinitionManager SettingDefinitionManager { get; }

    public OAuthSettingAppService(
        ISettingManager settingManager,
        IPermissionChecker permissionChecker,
        ISettingDefinitionManager settingDefinitionManager)
    {
        SettingManager = settingManager;
        PermissionChecker = permissionChecker;
        SettingDefinitionManager = settingDefinitionManager;

        LocalizationResource = typeof(OAuthResource);
    }

    public async virtual Task<SettingGroupResult> GetAllForCurrentTenantAsync()
    {
        return await GetAllForProviderAsync(TenantSettingValueProvider.ProviderName, CurrentTenant.GetId().ToString());
    }

    public async virtual Task<SettingGroupResult> GetAllForGlobalAsync()
    {
        return await GetAllForProviderAsync(GlobalSettingValueProvider.ProviderName, null);
    }

    protected async virtual Task<SettingGroupResult> GetAllForProviderAsync(string providerName, string providerKey)
    {
        var settingGroups = new SettingGroupResult();

        // 无权限返回空结果,直接报错的话,网关聚合会抛出异常
        if (await PermissionChecker.IsGrantedAsync(OAuthSettingPermissionNames.Settings))
        {
            var settingGroup = new SettingGroupDto(L["DisplayName:OAuth"], L["DisplayName:OAuth"]);

            #region Github OAuth链接

            var githubSetting = settingGroup.AddSetting(
                L["DisplayName:OAuth.GithubConnect"], L["Description:OAuth.GithubConnect"]);

            githubSetting.AddDetail(
                await SettingDefinitionManager.GetAsync(OAuthGithubSettingNames.GithubConnect.ClientId),
                StringLocalizerFactory,
                await SettingManager.GetOrNullAsync(OAuthGithubSettingNames.GithubConnect.ClientId, providerName, providerKey),
                ValueType.String,
                providerName);
            githubSetting.AddDetail(
                await SettingDefinitionManager.GetAsync(OAuthGithubSettingNames.GithubConnect.ClientSecret),
                StringLocalizerFactory,
                await SettingManager.GetOrNullAsync(OAuthGithubSettingNames.GithubConnect.ClientSecret, providerName, providerKey),
                ValueType.String,
                providerName);

            #endregion

            settingGroups.AddGroup(settingGroup);
        }

        return settingGroups;
    }

   
}
