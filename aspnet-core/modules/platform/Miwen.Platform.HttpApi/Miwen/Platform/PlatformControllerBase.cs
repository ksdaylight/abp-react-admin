using Miwen.Platform.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;

namespace Miwen.Platform;

public abstract class PlatformControllerBase : AbpControllerBase
{
    protected ISettingProvider SettingProvider => LazyServiceProvider.LazyGetRequiredService<ISettingProvider>();

    protected PlatformControllerBase()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}
