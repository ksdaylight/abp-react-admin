using System.Threading.Tasks;

namespace Miwen.Abp.SettingManagement;

public interface ISettingAppService : IReadonlySettingAppService
{
    Task SetGlobalAsync(UpdateSettingsDto input);

    Task SetCurrentTenantAsync(UpdateSettingsDto input);
}
