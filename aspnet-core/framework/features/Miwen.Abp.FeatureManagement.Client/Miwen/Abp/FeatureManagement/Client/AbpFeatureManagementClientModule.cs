using Miwen.Abp.FeatureManagement.Client;
using Miwen.Abp.FeatureManagement.Client.Permissions;
using Miwen.Abp.Features.Client;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.FeatureManagement.Localization;

namespace Miwen.Abp.FeatureManagement;

[DependsOn(
    typeof(AbpFeaturesClientModule),
    typeof(AbpFeatureManagementDomainModule)
    )]
public class AbpFeatureManagementClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpFeatureManagementClientModule>();
        });

        Configure<FeatureManagementOptions>(options =>
        {
            options.Providers.Add<ClientFeatureManagementProvider>();

            options.ProviderPolicies[ClientFeatureValueProvider.ProviderName] = ClientFeaturePermissionNames.ManageClientFeatures;
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpFeatureManagementResource>()
                .AddVirtualJson("/Miwen/Abp/FeatureManagement/Client/Localization/Resources");
        });
    }
}
