using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Miwen.Abp.Notifications;

public interface INotificationDefinitionGroupRecordRepository : IBasicRepository<NotificationDefinitionGroupRecord, Guid>
{
    Task<NotificationDefinitionGroupRecord> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
