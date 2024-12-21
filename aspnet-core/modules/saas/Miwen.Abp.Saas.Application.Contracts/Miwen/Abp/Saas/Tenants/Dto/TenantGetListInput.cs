using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Saas.Tenants;

public class TenantGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}