using Miwen.Abp.OAuth.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Miwen.Abp.OAuth.Github.Settings;

public class OAuthGithubSettingDefinitionProvider : Volo.Abp.Settings.SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(GetOAuthGithubSettings());
    }

    private SettingDefinition[] GetOAuthGithubSettings()
    {
        return new SettingDefinition[]
        {
            new SettingDefinition(
                OAuthGithubSettingNames.GithubConnect.ClientId, "",
                L("DisplayName:GithubConnect.ClientId"),
                L("Description:GithubConnect.ClientId"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
            new SettingDefinition(
                OAuthGithubSettingNames.GithubConnect.ClientSecret, "",
                L("DisplayName:GithubConnect.ClientSecret"),
                L("Description:GithubConnect.ClientSecret"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName)
        };
    }

    protected ILocalizableString L(string name)
    {
        return LocalizableString.Create<OAuthResource>(name);
    }
}
