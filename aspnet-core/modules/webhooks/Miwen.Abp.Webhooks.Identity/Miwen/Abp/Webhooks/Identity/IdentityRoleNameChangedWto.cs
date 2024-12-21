using System;

namespace Miwen.Abp.Webhooks.Identity;

[Serializable]
public class IdentityRoleNameChangedWto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string OldName { get; set; }
}
