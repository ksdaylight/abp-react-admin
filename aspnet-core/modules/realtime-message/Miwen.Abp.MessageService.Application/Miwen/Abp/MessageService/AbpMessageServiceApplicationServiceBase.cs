using Miwen.Abp.MessageService.Localization;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.MessageService;

public abstract class AbpMessageServiceApplicationServiceBase : ApplicationService
{
    protected AbpMessageServiceApplicationServiceBase()
    {
        LocalizationResource = typeof(MessageServiceResource);
        ObjectMapperContext = typeof(AbpMessageServiceApplicationModule);
    }
}
