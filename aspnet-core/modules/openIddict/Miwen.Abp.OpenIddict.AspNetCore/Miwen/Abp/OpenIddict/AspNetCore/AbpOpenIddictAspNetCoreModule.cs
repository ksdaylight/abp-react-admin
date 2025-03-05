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

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        AddOpenIddictWebProvidersClients(context.Services);
    }

    private void AddOpenIddictWebProvidersClients(IServiceCollection services)
    {
        //var openIddictBuilderServer = services.AddOpenIddict()
        //    .AddServer(builder =>
        //    {
        //        builder
        //        .SetTokenEndpointUris("connect/token","connect/external/external-login-callback");
        //        //"connect/external/callback/login",
        //        //注意这个尝试connect/external/external-login-callback变为可以允许token的方式，会强制要求是一个opiddict请求
        //        //也就是要有gran_type参数这些

        //        services.ExecutePreConfiguredActions(builder);
        //    });

        //services.ExecutePreConfiguredActions(openIddictBuilderServer);

        var openIddictBuilder = services.AddOpenIddict()
            .AddClient(options =>
             {
                 // Note: this sample only uses the authorization code flow,
                 // but you can enable the other flows if necessary.
                 options.AllowAuthorizationCodeFlow()
                    .AllowHybridFlow()
                    .AllowImplicitFlow()
                    .AllowPasswordFlow()
                    .AllowClientCredentialsFlow()
                    .AllowRefreshTokenFlow()
                    .AllowDeviceCodeFlow()
                    .AllowNoneFlow();

                 // Register the signing and encryption credentials used to protect
                 // sensitive data like the state tokens produced by OpenIddict.
                 options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                 // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                 options.UseAspNetCore()
                        .EnableRedirectionEndpointPassthrough()
                        .DisableTransportSecurityRequirement(); // 有需要可以自行开启

                 // Register the System.Net.Http integration.
                 //options.UseSystemNetHttp();

                 options.UseWebProviders()
                        .AddGitHub(options =>
                        {
                            options.SetClientId("9a885faa1f5696fc78f2")
                                   .SetClientSecret("691d8dc474b986647b5e22877256c2de253a1799")
                                   .SetRedirectUri("connect/external/callback/login/github")
                                   .SetProviderDisplayName("Log in with GitHub™️"); ;
                        });
             });
        services.ExecutePreConfiguredActions(openIddictBuilder);
    }
}
