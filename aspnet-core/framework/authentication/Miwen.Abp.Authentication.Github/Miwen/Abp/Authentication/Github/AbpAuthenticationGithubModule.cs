using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Miwen.Abp.OAuth.Github;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Authentication.Github;

[DependsOn(typeof(AbpOAuthGithubModule))]
public class AbpAuthenticationGithubModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services
            .AddAuthentication()
            .AddGithubConnect();
    }
}