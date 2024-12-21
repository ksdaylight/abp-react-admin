using Miwen.Abp.Exporter.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.Exporter;

[DependsOn(typeof(AbpLocalizationModule))]
public class AbpExporterCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpExporterCoreModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpExporterResource>("en")
                .AddVirtualJson("/Miwen/Abp/Exporter/Localization/Resources");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(ExporterErrorCodes.Namespace, typeof(AbpExporterResource));
        });
    }
}