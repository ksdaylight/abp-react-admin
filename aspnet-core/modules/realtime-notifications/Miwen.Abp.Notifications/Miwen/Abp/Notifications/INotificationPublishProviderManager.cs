using System.Collections.Generic;

namespace Miwen.Abp.Notifications;

public interface INotificationPublishProviderManager
{
    List<INotificationPublishProvider> Providers { get; }
}
