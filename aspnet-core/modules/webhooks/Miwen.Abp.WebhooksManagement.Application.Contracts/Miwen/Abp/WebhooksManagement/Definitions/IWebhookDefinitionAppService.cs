using Miwen.Abp.WebhooksManagement.Definitions.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.WebhooksManagement.Definitions;

public interface IWebhookDefinitionAppService : IApplicationService
{
    Task<WebhookDefinitionDto> GetAsync(string name);

    Task DeleteAsync(string name);

    Task<WebhookDefinitionDto> CreateAsync(WebhookDefinitionCreateDto input);

    Task<WebhookDefinitionDto> UpdateAsync(string name, WebhookDefinitionUpdateDto input);

    Task<ListResultDto<WebhookDefinitionDto>> GetListAsync(WebhookDefinitionGetListInput input);
}
