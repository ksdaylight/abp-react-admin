using Miwen.Platform.Routes;
using Volo.Abp.Validation;

namespace Miwen.Platform.Menus;
public class UserFavoriteMenuGetListInput
{
    [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
    public string Framework { get; set; }
}
