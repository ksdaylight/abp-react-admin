using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement.Localization;

namespace Miwen.Abp.SettingManagement;
public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpSettingManagementApplicationModule);
        LocalizationResource = typeof(AbpSettingManagementResource);
    }
}
