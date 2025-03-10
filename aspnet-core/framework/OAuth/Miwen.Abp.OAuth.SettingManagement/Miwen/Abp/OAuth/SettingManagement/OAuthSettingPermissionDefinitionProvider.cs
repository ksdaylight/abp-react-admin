using Miwen.Abp.OAuth.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Miwen.Abp.OAuth.SettingManagement;

public class OAuthSettingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var wechatGroup = context.AddGroup(
            OAuthSettingPermissionNames.GroupName,
            L("Permission:OAuth"));

        wechatGroup.AddPermission(
            OAuthSettingPermissionNames.Settings, L("Permission:OAuth.Settings"));
    }

    protected LocalizableString L(string name)
    {
        return LocalizableString.Create<OAuthResource>(name);
    }
}
