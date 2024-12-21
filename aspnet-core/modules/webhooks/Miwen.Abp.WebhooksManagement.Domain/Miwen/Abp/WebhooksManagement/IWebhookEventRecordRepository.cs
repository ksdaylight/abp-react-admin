using System;
using Volo.Abp.Domain.Repositories;

namespace Miwen.Abp.WebhooksManagement;

public interface IWebhookEventRecordRepository : IRepository<WebhookEventRecord, Guid>
{
}
