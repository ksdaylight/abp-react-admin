using Miwen.Platform.Routes;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Platform.Menus;

public class MenuGetByRoleInput
{
    [Required]
    [StringLength(80)]
    public string Role { get; set; }

    [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
    public string Framework { get; set; }
}
