using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace Miwen.Abp.OpenIddict.ExternalLogin;

[DependsOn(
    typeof(AbpOpenIddictAspNetCoreModule))]
public class AbpOpenIddictExternalLoginModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.AllowExternalLoginFlow();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.TryAdd(
                ExternalLoginTokenExtensionGrantConsts.GrantType,
                new ExternalLoginTokenExtensionGrant());
        });
    }
}
