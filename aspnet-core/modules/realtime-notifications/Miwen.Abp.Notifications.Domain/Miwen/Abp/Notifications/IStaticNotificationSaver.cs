using System.Threading.Tasks;

namespace Miwen.Abp.Notifications;
public interface IStaticNotificationSaver
{
    Task SaveAsync();
}
