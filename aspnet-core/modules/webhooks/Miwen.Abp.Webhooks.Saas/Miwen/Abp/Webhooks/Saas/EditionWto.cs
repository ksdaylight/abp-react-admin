using System;

namespace Miwen.Abp.Webhooks.Saas;

[Serializable]
public class EditionWto
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; }
}
