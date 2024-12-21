using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Platform.Menus;

public class MenuCreateDto : MenuCreateOrUpdateDto
{
    [Required]
    public Guid LayoutId { get; set; }
}
