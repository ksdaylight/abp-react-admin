using JetBrains.Annotations;
using Miwen.Abp.Webhooks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miwen.Abp.WebhooksManagement;

public interface IWebhookDefinitionSerializer
{
    Task<(WebhookGroupDefinitionRecord[], WebhookDefinitionRecord[])>
        SerializeAsync(IEnumerable<WebhookGroupDefinition> WebhookGroups);

    Task<WebhookGroupDefinitionRecord> SerializeAsync(
        WebhookGroupDefinition WebhookGroup);

    Task<WebhookDefinitionRecord> SerializeAsync(
        WebhookDefinition Webhook,
        [CanBeNull] WebhookGroupDefinition WebhookGroup);
}