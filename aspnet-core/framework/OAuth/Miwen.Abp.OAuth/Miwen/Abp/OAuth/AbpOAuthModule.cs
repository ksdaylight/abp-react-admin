using Microsoft.Extensions.DependencyInjection;
using Miwen.Abp.OAuth.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.OAuth.Github;

[DependsOn(typeof(AbpLocalizationModule))]
public class AbpOAuthModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpOAuthModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<OAuthResource>()
                .AddVirtualJson("/Miwen/Abp/OAuth/Localization/Resources");
        });

    }
}
