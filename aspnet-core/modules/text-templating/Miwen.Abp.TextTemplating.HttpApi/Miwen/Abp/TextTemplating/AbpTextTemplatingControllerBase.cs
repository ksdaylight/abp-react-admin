using Miwen.Abp.TextTemplating.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.TextTemplating;

public abstract class AbpTextTemplatingControllerBase : AbpControllerBase
{
    protected AbpTextTemplatingControllerBase()
    {
        LocalizationResource = typeof(AbpTextTemplatingResource);
    }
}
