using Elsa;
using Elsa.Options;
using Miwen.Abp.Elsa.Activities.BlobStoring;
using Miwen.Abp.Elsa.Activities.Emailing;
using Miwen.Abp.Elsa.Activities.IM;
using Miwen.Abp.Elsa.Activities.Notifications;
using Miwen.Abp.Elsa.Activities.Sms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Elsa.Activities;

[DependsOn(
    typeof(AbpElsaModule),
    typeof(AbpElsaActivitiesBlobStoringModule),
    typeof(AbpElsaActivitiesEmailingModule),
    typeof(AbpElsaActivitiesIMModule),
    typeof(AbpElsaActivitiesNotificationsModule),
    typeof(AbpElsaActivitiesSmsModule))]
public class AbpElsaActivitiesModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var startups = new[]
        {
            typeof(Emailing.Startup),
            typeof(BlobStoring.Startup),
            typeof(Notifications.Startup),
            typeof(Sms.Startup),
            typeof(IM.Startup),
            typeof(Webhooks.Startup),
        };

        PreConfigure<ElsaOptionsBuilder>(elsa =>
        {
            elsa.AddFeatures(startups, configuration);
        });
    }
}
