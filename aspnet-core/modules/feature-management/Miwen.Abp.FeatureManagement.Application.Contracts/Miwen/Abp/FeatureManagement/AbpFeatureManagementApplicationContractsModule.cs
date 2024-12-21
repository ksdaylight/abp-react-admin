using Volo.Abp.FeatureManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using VoloAbpFeatureManagementApplicationContractsModule = Volo.Abp.FeatureManagement.AbpFeatureManagementApplicationContractsModule;

namespace Miwen.Abp.FeatureManagement;

[DependsOn(
    typeof(VoloAbpFeatureManagementApplicationContractsModule))]
public class AbpFeatureManagementApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpFeatureManagementApplicationContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpFeatureManagementResource>()
                .AddVirtualJson("/Miwen/Abp/FeatureManagement/Localization/Application/Contracts");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(FeatureManagementErrorCodes.Namespace, typeof(AbpFeatureManagementResource));
        });
    }
}
