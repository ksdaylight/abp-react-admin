using System;

namespace Miwen.Abp.Webhooks.Identity;

[Serializable]
public class OrganizationUnitWto
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }
}
