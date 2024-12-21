using Elsa.Options;
using Miwen.Abp.Elsa.Activities.Webhooks;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebhooksServiceCollectionExtensions
{
    public static ElsaOptionsBuilder AddWebhooksActivities(this ElsaOptionsBuilder options)
    {
        options.AddActivity<PublishWebhook>();

        return options;
    }
}
