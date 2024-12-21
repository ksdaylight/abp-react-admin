using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Identity;

public class OrganizationUnitGetUnaddedRoleByPagedDto : PagedAndSortedResultRequestDto
{

    public string Filter { get; set; }
}
