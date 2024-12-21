using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.LocalizationManagement;

public interface IResourceAppService : IApplicationService
{
    Task<ResourceDto> GetByNameAsync(string name);

    Task<ResourceDto> CreateAsync(ResourceCreateDto input);

    Task<ResourceDto> UpdateAsync(string name, ResourceUpdateDto input);

    Task DeleteAsync(string name);
}
