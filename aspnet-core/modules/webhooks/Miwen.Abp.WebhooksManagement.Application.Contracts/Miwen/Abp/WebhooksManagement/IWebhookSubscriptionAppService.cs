using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.WebhooksManagement;

public interface IWebhookSubscriptionAppService :
    ICrudAppService<
        WebhookSubscriptionDto,
        Guid,
        WebhookSubscriptionGetListInput,
        WebhookSubscriptionCreateInput,
        WebhookSubscriptionUpdateInput>
{
    Task DeleteManyAsync(WebhookSubscriptionDeleteManyInput input);

    Task<ListResultDto<WebhookAvailableGroupDto>> GetAllAvailableWebhooksAsync();
}
