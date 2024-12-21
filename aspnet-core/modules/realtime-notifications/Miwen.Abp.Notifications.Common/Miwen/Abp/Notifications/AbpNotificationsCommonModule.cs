using Miwen.Abp.Notifications.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Miwen.Abp.Notifications.Common;

[DependsOn(
    typeof(AbpNotificationsModule))]
public class AbpNotificationsCommonModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpNotificationsCommonModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<NotificationsResource>()
                .AddVirtualJson("/Miwen/Abp/Notifications/Localization/Common");
        });
    }
}
