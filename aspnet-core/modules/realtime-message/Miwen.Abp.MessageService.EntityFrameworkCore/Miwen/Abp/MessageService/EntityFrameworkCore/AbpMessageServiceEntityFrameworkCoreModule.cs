using Miwen.Abp.MessageService.Chat;
using Miwen.Abp.MessageService.Groups;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.MessageService.EntityFrameworkCore;

[DependsOn(
    typeof(AbpMessageServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class AbpMessageServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MessageServiceDbContext>(options =>
        {
            options.AddDefaultRepositories<IMessageServiceDbContext>();

            options.AddRepository<ChatGroup, EfCoreGroupRepository>();
            options.AddRepository<UserChatGroup, EfCoreUserChatGroupRepository>();
            options.AddRepository<UserChatCard, EfCoreUserChatCardRepository>();
            options.AddRepository<UserChatSetting, EfCoreUserChatSettingRepository>();

            options.AddRepository<UserChatFriend, EfCoreUserChatFriendRepository>();
        });
    }
}
