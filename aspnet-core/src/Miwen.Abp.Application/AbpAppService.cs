using Miwen.Abp.Localization;
using Volo.Abp.Application.Services;

namespace Miwen.Abp;

/* Inherit your application services from this class.
 */
public abstract class AbpAppService : ApplicationService
{
    protected AbpAppService()
    {
        LocalizationResource = typeof(AbpResource);
    }
}
