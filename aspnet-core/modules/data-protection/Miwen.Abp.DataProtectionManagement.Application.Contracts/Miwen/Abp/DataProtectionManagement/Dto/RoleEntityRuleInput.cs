using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.DataProtectionManagement;
public class RoleEntityRuleInput : EntityRuleCreateOrUpdateDto
{
    [Required]
    public Guid RoleId { get; set; }

    [Required]
    [DynamicStringLength(typeof(RoleEntityRuleConsts), nameof(RoleEntityRuleConsts.MaxRuletNameLength))]
    public string RoleName { get; set; }
}
