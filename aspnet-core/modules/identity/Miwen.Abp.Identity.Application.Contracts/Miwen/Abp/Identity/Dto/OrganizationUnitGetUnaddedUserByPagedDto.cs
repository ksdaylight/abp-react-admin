using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Identity;

public class OrganizationUnitGetUnaddedUserByPagedDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
