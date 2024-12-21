using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Miwen.Abp.Notifications;

public interface INotificationDefinitionRecordRepository : IBasicRepository<NotificationDefinitionRecord, Guid>
{
    Task<NotificationDefinitionRecord> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}
