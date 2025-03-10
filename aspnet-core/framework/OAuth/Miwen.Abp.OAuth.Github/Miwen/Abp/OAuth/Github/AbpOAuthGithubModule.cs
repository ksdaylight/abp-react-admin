using Microsoft.Extensions.DependencyInjection;
using Miwen.Abp.OAuth.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.OAuth.Github;

[DependsOn(typeof(AbpOAuthModule))]
public class AbpOAuthGithubModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpOAuthGithubModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<OAuthResource>()
                .AddVirtualJson("/Miwen/Abp/OAuth/Github/Localization/Resources");
        });

        context.Services.AddAbpDynamicOptions<AbpOAuthGithubOptions, AbpOAuthGithubOptionsManager>();
    }
}
