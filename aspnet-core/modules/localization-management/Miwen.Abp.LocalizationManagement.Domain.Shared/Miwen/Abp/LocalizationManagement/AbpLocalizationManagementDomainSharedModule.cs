using Miwen.Abp.LocalizationManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.LocalizationManagement;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpLocalizationModule))]
public class AbpLocalizationManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpLocalizationManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LocalizationManagementResource>("en")
                .AddVirtualJson("/Miwen/Abp/LocalizationManagement/Localization/Resources");
        });
        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(LocalizationErrorCodes.Namespace, typeof(LocalizationManagementResource));
        });
    }
}
