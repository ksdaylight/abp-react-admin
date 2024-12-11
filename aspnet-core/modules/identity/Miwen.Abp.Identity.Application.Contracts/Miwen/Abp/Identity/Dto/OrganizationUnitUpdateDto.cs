using Volo.Abp.ObjectExtending;

namespace Miwen.Abp.Identity;

public class OrganizationUnitUpdateDto : ExtensibleObject
{
    public string DisplayName { get; set; }
}
