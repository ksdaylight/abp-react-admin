using System;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Miwen.Abp.WebhooksManagement;

[Serializable]
[EventName("abp.webhooks.subscription")]
public class WebhookSubscriptionEto : IMultiTenant
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string WebhookUri { get; set; }
}
