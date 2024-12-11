using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Identity;

public class OrganizationUnitGetByPagedDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
