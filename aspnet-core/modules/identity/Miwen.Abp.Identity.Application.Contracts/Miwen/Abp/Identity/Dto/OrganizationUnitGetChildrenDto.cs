using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Identity;

public class OrganizationUnitGetChildrenDto : IEntityDto<Guid>
{
    [Required]
    public Guid Id { get; set; }
    public bool Recursive { get; set; }
}
