using Miwen.Platform.Routes;
using Volo.Abp.EventBus;

namespace Miwen.Platform.Menus;

[EventName("platform.menus.menu")]
public class MenuEto : RouteEto
{
    public string Framework { get; set; }
}
