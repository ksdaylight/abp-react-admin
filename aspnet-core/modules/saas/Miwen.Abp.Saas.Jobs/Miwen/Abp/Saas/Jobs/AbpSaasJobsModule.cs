using Miwen.Abp.BackgroundTasks;
using Miwen.Abp.Saas.Localization;
using Volo.Abp.Emailing;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.Saas.Jobs;

[DependsOn(typeof(AbpEmailingModule))]
[DependsOn(typeof(AbpSaasDomainModule))]
[DependsOn(typeof(AbpBackgroundTasksAbstractionsModule))]
public class AbpSaasJobsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpSaasJobsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpSaasResource>()
                .AddVirtualJson("/Miwen/Abp/Saas/Jobs/Localization/Resources");
        });
    }
}
