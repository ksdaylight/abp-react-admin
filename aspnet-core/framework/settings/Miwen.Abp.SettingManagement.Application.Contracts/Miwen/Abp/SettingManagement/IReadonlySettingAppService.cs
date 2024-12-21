using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.SettingManagement;

public interface IReadonlySettingAppService : IApplicationService
{
    Task<SettingGroupResult> GetAllForGlobalAsync();

    Task<SettingGroupResult> GetAllForCurrentTenantAsync();
}
