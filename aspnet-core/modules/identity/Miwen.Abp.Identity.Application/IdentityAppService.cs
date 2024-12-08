using Miwen.Abp.Identity.Localization;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.Identity;

public abstract class IdentityAppService : ApplicationService
{
    protected IdentityAppService()
    {
        LocalizationResource = typeof(IdentityResource);
        ObjectMapperContext = typeof(IdentityApplicationModule);
    }
}
