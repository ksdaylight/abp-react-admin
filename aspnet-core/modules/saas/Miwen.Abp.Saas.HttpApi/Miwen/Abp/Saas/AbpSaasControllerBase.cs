using Miwen.Abp.Saas.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.Saas;
public abstract class AbpSaasControllerBase : AbpControllerBase
{
    protected AbpSaasControllerBase()
    {
        LocalizationResource = typeof(AbpSaasResource);
    }
}
