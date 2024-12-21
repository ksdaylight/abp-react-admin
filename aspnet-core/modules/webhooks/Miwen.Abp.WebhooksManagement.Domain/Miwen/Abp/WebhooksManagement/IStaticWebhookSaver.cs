using System.Threading.Tasks;

namespace Miwen.Abp.WebhooksManagement;

public interface IStaticWebhookSaver
{
    Task SaveAsync();
}
