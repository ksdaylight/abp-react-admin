using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.DataProtectionManagement;
public class RoleEntityRuleCreateDto : EntityRuleCreateOrUpdateDto
{
    [Required]
    public Guid EntityTypeId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    [Required]
    [DynamicStringLength(typeof(RoleEntityRuleConsts), nameof(RoleEntityRuleConsts.MaxRuletNameLength))]
    public string RoleName { get; set; }
}
