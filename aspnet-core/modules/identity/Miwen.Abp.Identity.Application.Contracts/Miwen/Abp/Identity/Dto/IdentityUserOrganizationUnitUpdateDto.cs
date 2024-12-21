using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.Identity;

public class IdentityUserOrganizationUnitUpdateDto
{
    [Required]
    public Guid[] OrganizationUnitIds { get; set; }
}
