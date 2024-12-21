using Miwen.Abp.OssManagement.Localization;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.OssManagement;

public abstract class OssManagementApplicationServiceBase : ApplicationService
{
    protected OssManagementApplicationServiceBase()
    {
        LocalizationResource = typeof(AbpOssManagementResource);
        ObjectMapperContext = typeof(AbpOssManagementApplicationModule);
    }
}
