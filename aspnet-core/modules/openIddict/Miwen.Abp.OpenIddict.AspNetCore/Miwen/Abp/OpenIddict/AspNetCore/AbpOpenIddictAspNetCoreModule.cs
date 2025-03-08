using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Miwen.Abp.Identity;
using OpenIddict.Abstractions;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.WildcardDomains;
using Volo.Abp.OpenIddict;
using VoloAbpOpenIddictAspNetCoreModule = Volo.Abp.OpenIddict.AbpOpenIddictAspNetCoreModule;

namespace Miwen.Abp.OpenIddict.AspNetCore;

[DependsOn(
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(VoloAbpOpenIddictAspNetCoreModule))]
public class AbpOpenIddictAspNetCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.RegisterClaims(new[] { IdentityConsts.ClaimType.Avatar.Name });
        });
    }
}
