using Miwen.Abp.BackgroundTasks.Activities;
using Miwen.Abp.BackgroundTasks.Localization;
using Miwen.Abp.Notifications;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.BackgroundTasks.Notifications;

[DependsOn(
    typeof(AbpBackgroundTasksActivitiesModule),
    typeof(AbpNotificationsModule))]
public class AbpBackgroundTasksNotificationsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpBackgroundTasksNotificationsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BackgroundTasksResource>()
                .AddVirtualJson("/Miwen/Abp/BackgroundTasks/Notifications/Localization/Resources");
        });
    }
}
