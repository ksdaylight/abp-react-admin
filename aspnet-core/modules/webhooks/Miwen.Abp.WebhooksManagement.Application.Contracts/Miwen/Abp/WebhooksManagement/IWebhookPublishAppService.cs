using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.WebhooksManagement;

public interface IWebhookPublishAppService : IApplicationService
{
    Task PublishAsync(WebhookPublishInput input);
}
