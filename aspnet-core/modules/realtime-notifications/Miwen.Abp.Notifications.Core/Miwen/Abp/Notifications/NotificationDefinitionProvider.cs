using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.Notifications;

public abstract class NotificationDefinitionProvider : INotificationDefinitionProvider, ITransientDependency
{
    public abstract void Define(INotificationDefinitionContext context);
}
