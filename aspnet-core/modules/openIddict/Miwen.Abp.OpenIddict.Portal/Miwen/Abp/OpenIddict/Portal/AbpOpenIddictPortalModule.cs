//using Miwen.Platform;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace Miwen.Abp.OpenIddict.Portal;

[DependsOn(
    typeof(AbpOpenIddictAspNetCoreModule),
    //typeof(PlatformDomainModule),
    typeof(AbpAspNetCoreMultiTenancyModule))]
public class AbpOpenIddictPortalModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.AllowPortalFlow();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.TryAdd(
                PortalTokenExtensionGrantConsts.GrantType,
                new PortalTokenExtensionGrant());
        });
    }
}
