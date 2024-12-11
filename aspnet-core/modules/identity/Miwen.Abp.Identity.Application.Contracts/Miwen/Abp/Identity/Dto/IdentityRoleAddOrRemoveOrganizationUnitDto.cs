using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.Identity;

public class IdentityRoleAddOrRemoveOrganizationUnitDto
{
    [Required]
    public Guid[] OrganizationUnitIds { get; set; }
}
