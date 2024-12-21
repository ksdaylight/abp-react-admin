using Miwen.Abp.Saas.Localization;
using Volo.Abp.Localization;

namespace Miwen.Abp.Saas.Jobs;

internal static class LocalizableStatic
{
    public static ILocalizableString Create(string name)
    {
        return LocalizableString.Create<AbpSaasResource>(name);
    }
}
