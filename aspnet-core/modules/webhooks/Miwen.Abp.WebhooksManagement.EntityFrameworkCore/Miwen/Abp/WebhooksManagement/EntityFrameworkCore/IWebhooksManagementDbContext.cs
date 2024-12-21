using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Miwen.Abp.WebhooksManagement.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(WebhooksManagementDbProperties.ConnectionStringName)]
public interface IWebhooksManagementDbContext : IEfCoreDbContext
{
    DbSet<WebhookSendRecord> WebhookSendRecord { get; }
    DbSet<WebhookGroupDefinitionRecord> WebhookGroupDefinitionRecords { get; }
    DbSet<WebhookDefinitionRecord> WebhookDefinitionRecords { get; }
}
