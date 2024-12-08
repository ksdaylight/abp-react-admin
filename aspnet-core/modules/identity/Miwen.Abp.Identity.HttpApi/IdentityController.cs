using Miwen.Abp.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.Identity;

public abstract class IdentityController : AbpControllerBase
{
    protected IdentityController()
    {
        LocalizationResource = typeof(IdentityResource);
    }
}
