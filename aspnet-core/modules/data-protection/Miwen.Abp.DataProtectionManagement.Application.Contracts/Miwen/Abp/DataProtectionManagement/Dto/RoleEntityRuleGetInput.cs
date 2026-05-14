using Miwen.Abp.DataProtection;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.DataProtectionManagement;

public class RoleEntityRuleGetInput
{
    [Required]
    [DynamicStringLength(typeof(RoleEntityRuleConsts), nameof(RoleEntityRuleConsts.MaxRuletNameLength))]
    public string RoleName { get; set; }

    [Required]
    public Guid EntityTypeId { get; set; }

    [Required]
    public DataAccessOperation Operation { get; set; }
}
