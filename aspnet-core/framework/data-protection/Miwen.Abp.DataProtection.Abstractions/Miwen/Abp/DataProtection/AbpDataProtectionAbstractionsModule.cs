using Miwen.Abp.DataProtection.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.DataProtection;

[DependsOn(typeof(AbpLocalizationModule))]
[DependsOn(typeof(AbpObjectExtendingModule))]
public class AbpDataProtectionAbstractionsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpDataProtectionAbstractionsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DataProtectionResource>()
                .AddVirtualJson("/Miwen/Abp/DataProtection/Localization/Resources");
        });
    }
}
