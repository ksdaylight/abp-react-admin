using Miwen.Abp.IM.Localization;
using Miwen.Abp.MessageService.Chat;
using Miwen.Abp.MessageService.Localization;
using Miwen.Abp.MessageService.ObjectExtending;
using Miwen.Abp.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;

namespace Miwen.Abp.MessageService;

[DependsOn(
    typeof(AbpMapperlyModule),
    typeof(AbpCachingModule),
    typeof(AbpNotificationsModule),
    typeof(AbpMessageServiceDomainSharedModule))]
public class AbpMessageServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpMessageServiceDomainModule>();

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MessageServiceResource>()
                .AddBaseTypes(typeof(AbpIMResource));
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
            MessageServiceModuleExtensionConsts.ModuleName,
            MessageServiceModuleExtensionConsts.EntityNames.Message,
            typeof(Message)
        );
    }
}
