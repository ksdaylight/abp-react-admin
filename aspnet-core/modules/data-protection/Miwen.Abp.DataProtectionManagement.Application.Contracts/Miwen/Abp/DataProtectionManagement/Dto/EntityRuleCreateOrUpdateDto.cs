using Miwen.Abp.DataProtection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.DataProtectionManagement;
public abstract class EntityRuleCreateOrUpdateDto
{
    public bool IsEnabled { get; set; }

    [Required]
    public DataAccessOperation Operation { get; set; }

    [Required]
    public DataAccessFilterGroup FilterGroup { get; set; }

    public string[] AllowProperties { get; set; }
}
