using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Identity;

public class IdentityClaimTypeGetByPagedDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
