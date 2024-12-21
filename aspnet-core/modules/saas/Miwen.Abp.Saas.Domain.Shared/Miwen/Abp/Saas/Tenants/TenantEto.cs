using System;
using Volo.Abp.Auditing;
using Volo.Abp.EventBus;

namespace Miwen.Abp.Saas.Tenants;

[Serializable]
[EventName("abp.saas.tenant")]
public class TenantEto : IHasEntityVersion
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int EntityVersion { get; set; }
}
