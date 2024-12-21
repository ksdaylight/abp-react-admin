using System;
using Volo.Abp.EventBus;

namespace Miwen.Abp.DataProtectionManagement;

[Serializable]
[EventName("abp.data_protection.entity_rule.role")]
public class RoleEntityRuleEto : EntityRuleBaseEto
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}
