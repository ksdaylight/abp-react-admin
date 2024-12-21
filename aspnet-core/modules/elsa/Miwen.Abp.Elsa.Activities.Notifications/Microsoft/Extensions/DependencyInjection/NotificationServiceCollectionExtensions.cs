using Elsa.Options;
using Miwen.Abp.Elsa.Activities.Notifications;

namespace Microsoft.Extensions.DependencyInjection;

public static class NotificationServiceCollectionExtensions
{
    public static ElsaOptionsBuilder AddNotificationActivities(this ElsaOptionsBuilder options)
    {
        options
            .AddActivity<SendNotification>();

        return options;
    }
}
