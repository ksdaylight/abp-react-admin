using System;

namespace Miwen.Abp.Webhooks.Identity;

[Serializable]
public class IdentityRoleWto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
