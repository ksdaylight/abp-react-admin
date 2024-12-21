using Volo.Abp.Domain.Entities;

namespace Miwen.Platform.Menus;

public class UserFavoriteMenuUpdateDto : UserFavoriteMenuCreateOrUpdateDto, IHasConcurrencyStamp
{

    public string ConcurrencyStamp { get; set; }
}
