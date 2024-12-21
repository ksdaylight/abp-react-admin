using Miwen.Abp.BackgroundTasks.Activities;
using Miwen.Abp.BackgroundTasks.Localization;
using Volo.Abp.Emailing;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.BackgroundTasks.ExceptionHandling;

[DependsOn(typeof(AbpBackgroundTasksActivitiesModule))]
[DependsOn(typeof(AbpEmailingModule))]
public class AbpBackgroundTasksExceptionHandlingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpBackgroundTasksExceptionHandlingModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BackgroundTasksResource>()
                .AddVirtualJson("/Miwen/Abp/BackgroundTasks/ExceptionHandling/Localization/Resources");
        });
    }
}
