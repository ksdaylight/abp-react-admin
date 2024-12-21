using Miwen.Abp.WebhooksManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.WebhooksManagement;

public abstract class WebhooksManagementControllerBase : AbpControllerBase
{
    protected WebhooksManagementControllerBase()
    {
        LocalizationResource = typeof(WebhooksManagementResource);
    }
}
