using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Platform.Menus;
public class UserFavoriteMenuRemoveInput
{
    [Required]
    public Guid MenuId { get; set; }
}
