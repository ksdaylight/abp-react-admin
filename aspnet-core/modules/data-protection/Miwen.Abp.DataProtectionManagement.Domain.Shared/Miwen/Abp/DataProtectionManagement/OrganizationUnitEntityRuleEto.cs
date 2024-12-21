using System;
using Volo.Abp.EventBus;

namespace Miwen.Abp.DataProtectionManagement;

[Serializable]
[EventName("abp.data_protection.entity_rule.organization_unit")]
public class OrganizationUnitEntityRuleEto : EntityRuleBaseEto
{
    public Guid OrgId { get; set; }
    public string OrgCode { get; set; }
}
