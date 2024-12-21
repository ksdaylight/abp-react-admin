namespace Miwen.Abp.Notifications;

public interface INotificationDefinitionProvider
{
    void Define(INotificationDefinitionContext context);
}
