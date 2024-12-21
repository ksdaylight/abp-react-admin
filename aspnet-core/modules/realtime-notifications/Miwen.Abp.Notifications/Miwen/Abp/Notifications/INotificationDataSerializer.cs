namespace Miwen.Abp.Notifications;
public interface INotificationDataSerializer
{
    NotificationData Serialize(NotificationData source);
}
