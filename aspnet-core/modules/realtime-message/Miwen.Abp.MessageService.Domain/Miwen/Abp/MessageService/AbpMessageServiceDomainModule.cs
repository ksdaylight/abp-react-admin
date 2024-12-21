using Miwen.Abp.IM.Localization;
using Miwen.Abp.MessageService.Chat;
using Miwen.Abp.MessageService.Localization;
using Miwen.Abp.MessageService.Mapper;
using Miwen.Abp.MessageService.ObjectExtending;
using Miwen.Abp.Notifications;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;

namespace Miwen.Abp.MessageService;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule),
    typeof(AbpNotificationsModule),
    typeof(AbpMessageServiceDomainSharedModule))]
public class AbpMessageServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<MessageServiceDomainAutoMapperProfile>(validate: true);
        });

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
